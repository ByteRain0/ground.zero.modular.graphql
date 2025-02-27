using Anime.Contracts.Services.Anime.Queries;
using GreenDonut.Data;
using MediatR;

namespace Anime.GraphQL.Anime.Queries;

[QueryType]
public static class GetAnimeByTitleQuery
{
    public static async Task<Contracts.Models.Anime?> GetAnimeByTitleAsync(
        string title,
        QueryContext<Contracts.Models.Anime> queryContext,
        CancellationToken cancellationToken,
        IMediator mediator)
        => await mediator.Send(new GetAnimeByTitle(title, queryContext), cancellationToken);
}