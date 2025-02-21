using Anime.Contracts.Services.Anime.Queries;
using GreenDonut.Data;
using HotChocolate.Types.Pagination;
using MediatR;

namespace Anime.GraphQL.Anime.Queries;

[QueryType]
public static class GetAnimeQuery
{
    [UsePaging]
    public static async Task<Connection<Contracts.Models.Anime>> GetAnimeAsync(
        PagingArguments pagingArguments,
        CancellationToken cancellationToken,
        [Service] IMediator mediator) =>
        (await mediator.Send(new GetAnime(pagingArguments, new GetAnimeQueryFilters()), cancellationToken)).ToConnection();
}