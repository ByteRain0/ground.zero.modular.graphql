using System.Globalization;
using Core.Otel;
using MediatR;
using Rating.Api.Domain;
using Rating.Api.Infrastructure;
using StackExchange.Redis;

namespace Rating.Api.Application.Rating.GetRatingByIdQuery;

public class GetRatingByIdHandler : IRequestHandler<GetRatingById, double>
{
    private readonly DatabaseMock _dbContext;
    private readonly IConnectionMultiplexer _connectionMux;

    public GetRatingByIdHandler(
        DatabaseMock dbContext,
        IConnectionMultiplexer connectionMux)
    {
        _dbContext = dbContext;
        _connectionMux = connectionMux;
    }

    public async Task<double> Handle(GetRatingById request, CancellationToken cancellationToken)
    {
        using var retrieveFromCacheActivity =
            RatingApiRunTimeDiagnosticConfig.Source.StartActivity("Retrieve rating from cache");
        var cacheKey = $"{request.EntityType}:{request.Id}";
        retrieveFromCacheActivity?.SetTag("cache-key", cacheKey);

        var db = _connectionMux.GetDatabase();
        var cachedValue = await db.StringGetAsync(cacheKey);

        if (cachedValue.HasValue)
        {
            return double.Parse(cachedValue!, CultureInfo.InvariantCulture);
        }

        retrieveFromCacheActivity?.Stop();

        using var retrieveFromDatabaseActivity =
            RatingApiRunTimeDiagnosticConfig.Source.StartActivity("Retrieve rating from database");

        if (!_dbContext.Ratings.Any(x => x.EntityType == request.EntityType && x.EntityId == request.Id))
        {
            var exception = new RatingNotFoundException(request.EntityType, request.Id);
            retrieveFromDatabaseActivity.AddExceptionAndFail(exception);
            throw exception;
        }

        var averageRating = _dbContext
            .Ratings
            .Where(x =>
                x.EntityType == request.EntityType
                && x.EntityId == request.Id)
            .Average(x => x.Mark);

        //Emulate a call to the DB
        Thread.Sleep(200);
        retrieveFromDatabaseActivity?.Stop();

        using var setRatingToCacheActivity =
            RatingApiRunTimeDiagnosticConfig.Source.StartActivity("Set rating to cache");
        setRatingToCacheActivity?.SetTag("cache-key", cacheKey);
        setRatingToCacheActivity?.SetTag("cache-value", averageRating.ToString(CultureInfo.InvariantCulture));

        await db.StringSetAsync(
            cacheKey,
            averageRating,
            TimeSpan.FromHours(1));

        return averageRating;
    }
}