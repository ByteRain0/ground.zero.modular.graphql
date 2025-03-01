using Anime.Contracts.Services.Anime.Queries;
using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Types.Pagination;
using MediatR;

namespace Anime.GraphQL.Anime.Queries;

[QueryType]
public static class GetAnimeQuery
{
    [UsePaging]
    [UseFiltering]
    public static async Task<Connection<Contracts.Models.Anime>> GetAnimeAsync(
        PagingArguments pagingArguments,
        QueryContext<Contracts.Models.Anime>? queryContext,
        CancellationToken cancellationToken,
        IMediator mediator) =>
        await mediator.Send(new GetAnime(pagingArguments,queryContext, new GetAnimeQueryFilters()), cancellationToken).ToConnectionAsync();
}


public class AnimeFilterInput : FilterInputType<Contracts.Models.Anime>
{
    protected override void Configure(IFilterInputTypeDescriptor<Contracts.Models.Anime> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(a => a.Title);
    }
}