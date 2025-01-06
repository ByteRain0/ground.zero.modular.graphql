using System.Globalization;
using MediatR;
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
        var cacheKey = $"{request.EntityType}:{request.Id}";
        var db = _connectionMux.GetDatabase();

        // Try to retrieve the average rating from the cache
        var cachedValue = await db.StringGetAsync(cacheKey);
        if (cachedValue.HasValue)
        {
            // If found in cache, return the cached value
            return double.Parse(cachedValue!);
        }
        
        // If not found in cache, calculate the average rating from the database
        var averageRating = _dbContext
            .Ratings
            .Where(x =>
                x.EntityType == request.EntityType
                && x.EntityId == request.Id)
            .Average(x => x.Mark);

        //Emulate a call to the DB
        Thread.Sleep(500);

        
        // Cache the calculated average rating with a sliding expiration of 1 hour
        await db.StringSetAsync(
            cacheKey,
            averageRating.ToString(CultureInfo.InvariantCulture),
            TimeSpan.FromHours(1));

        return averageRating;
    }
}
