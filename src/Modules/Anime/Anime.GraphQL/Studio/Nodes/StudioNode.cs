using Anime.Contracts.Services.Studio.Queries;
using MediatR;
using GreenDonut.Data;
using HotChocolate.Types.Pagination;

namespace Anime.GraphQL.Studio.Nodes;

[ObjectType<Contracts.Models.Studio>]
public static partial class StudioNode
{
    static partial void Configure(IObjectTypeDescriptor<Contracts.Models.Studio> descriptor)
    {
        descriptor.BindFieldsImplicitly();
    }

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<Connection<Contracts.Models.Anime>> GetAnimeAsync(
        [Parent(requires: nameof(Contracts.Models.Studio.Id))]
        Contracts.Models.Studio studio,
        PagingArguments pagingArguments,
        QueryContext<Contracts.Models.Anime> queryContext,
        CancellationToken cancellationToken,
        IMediator mediator) =>
        await mediator.Send(new GetAnimeByStudioId(
                StudioId: studio.Id,
                PagingArguments: pagingArguments,
                QueryContext: queryContext), cancellationToken)
            .ToConnectionAsync();
}