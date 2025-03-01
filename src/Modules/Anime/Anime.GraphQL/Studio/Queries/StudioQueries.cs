using Anime.Contracts.Services.Studio.Queries;
using MediatR;
using GreenDonut.Data;
using HotChocolate.Types.Pagination;

namespace Anime.GraphQL.Studio.Queries;

[QueryType]
public static class StudioQueries
{
    [UsePaging]
    public static async Task<Connection<Contracts.Models.Studio>> GetStudioAsync(
        PagingArguments pagingArguments,
        QueryContext<Contracts.Models.Studio>? queryContext,
        CancellationToken cancellationToken,
        IMediator mediator) =>
        await mediator.Send(new GetStudios(pagingArguments, queryContext), cancellationToken).ToConnectionAsync();

    [NodeResolver]
    public static async Task<Contracts.Models.Studio?> GetStudioByIdAsync(
        int id,
        QueryContext<Contracts.Models.Studio>? queryContext,
        CancellationToken cancellationToken,
        IMediator mediator) =>
        await mediator.Send(new GetStudioById(id, queryContext), cancellationToken);
}