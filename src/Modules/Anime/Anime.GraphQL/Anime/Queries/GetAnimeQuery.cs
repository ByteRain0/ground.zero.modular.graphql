using Anime.Contracts.Services.Anime.Queries;
using GreenDonut.Data;
using HotChocolate.Types.Pagination;
using MediatR;

namespace Anime.GraphQL.Anime.Queries;

[QueryType]
public static class GetAnimeQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<Connection<Contracts.Models.Anime>> GetAnimeAsync(
        PagingArguments pagingArguments,
        QueryContext<Contracts.Models.Anime>? queryContext,
        CancellationToken cancellationToken,
        IMediator mediator) =>
        await mediator.Send(new GetAnime(pagingArguments,queryContext), cancellationToken).ToConnectionAsync();
}