using Anime.Contracts.Services.Anime.Queries;
using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;
using MediatR;

namespace Anime.GraphQL.Studio.Nodes;

/// <summary>
/// Re-write the graph node / model that is exposed to the client application.
/// </summary>
[ObjectType<Contracts.Models.Studio>]
public static partial class StudioNode
{
    static partial void Configure(IObjectTypeDescriptor<Contracts.Models.Studio> descriptor)
    {
        descriptor.BindFieldsImplicitly();

        descriptor
            .Field(x => x.Id)
            .UseFiltering();

        descriptor
            .Field(x => x.Name)
            .UseFiltering();
    }
    
    /// <summary>
    /// Example of how one can extend the functionality of a node.
    /// </summary>
    /// <param name="studio"></param>
    /// <param name="pagingArguments"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="mediator"></param>
    /// <returns></returns>
    [UsePaging]
    public static async Task<Connection<Contracts.Models.Anime>> GetAnimeAsync(
        [Parent] Contracts.Models.Studio studio,
        PagingArguments pagingArguments,
        CancellationToken cancellationToken,
        [Service] IMediator mediator) =>
        (await mediator.Send(new GetAnime(pagingArguments, new GetAnimeQueryFilters()
        {
            StudioId = studio.Id
        }), cancellationToken)).ToConnection();
}