using Anime.Contracts.Services.Studio.Queries;
using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;
using MediatR;

namespace Anime.GraphQL.Studio.Queries;

[QueryType]
public static class StudioQueries
{
    [UsePaging] 
    public static async Task<Connection<Contracts.Models.Studio>> GetStudioAsync(
        PagingArguments pagingArguments,
        CancellationToken cancellationToken,
        [Service] IMediator mediator) =>
        (await mediator.Send(new GetStudios(pagingArguments), cancellationToken)).ToConnection();
    
    [NodeResolver]
    public static async Task<Contracts.Models.Studio?> GetStudioByIdAsync(
        int id,
        CancellationToken cancellationToken,
        [Service] IMediator mediator) =>
        await mediator.Send(new GetStudioById(id), cancellationToken);
}