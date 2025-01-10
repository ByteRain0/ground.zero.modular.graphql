using MediatR;
using Rating.Api.Application.Rating.GetRatingByIdQuery;

namespace Rating.Api.GraphQL.Queries;

[QueryType]
public static class RatingQuery
{
    public static async Task<double> GetRatingAsync(
        int id,
        string entityType,
        CancellationToken cancellationToken,
        IMediator mediator) =>
        await mediator.Send(new GetRatingById(id, entityType), cancellationToken);
}