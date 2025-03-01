using Anime.Contracts.Services.Anime.Queries;
using GreenDonut.Data;
using MediatR;

namespace Anime.GraphQL.Anime.Queries;

[QueryType]
public static class GetAnimeByIdQuery
{
    // In case we want to return exact errors we can use [Error<>].
    // Do not overuse it for query side.
    // Most fields are ok with just nullable return types.
    // [Error<AnimeNotFoundException>]
    //[Error<ForbiddenException>]
    [NodeResolver]
    public static async Task<Contracts.Models.Anime?> GetAnimeByIdAsync(
        // [ID<Anime> int id] alternative way to use the GraphQl_ID
        int id,
        QueryContext<Contracts.Models.Anime>? queryContext,
        CancellationToken cancellationToken,
        IMediator mediator)
        => await mediator.Send(new GetAnimeById(id, queryContext), cancellationToken);
}