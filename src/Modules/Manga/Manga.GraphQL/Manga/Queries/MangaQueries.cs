using HotChocolate.Data.Sorting;
using Manga.Contracts.Services.Manga.Queries;
using MediatR;

namespace Manga.GraphQL.Manga.Queries;

[QueryType]
public static class MangaQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Contracts.Models.Manga>> GetMangaAsync(
        [Service] IMediator mediator,
        ISortingContext sortingContext)
    {
        sortingContext.Handled(false);
        
        var queryBase = await mediator.Send(new GetManga());

        // Add default sorting if none specified.
        // Allow base ordering to be overwritten.
        if (!sortingContext.IsDefined)
        {
            queryBase
                .OrderBy(x => x.Title)
                .ThenBy(x => x.Id);
        }
        
        return queryBase;
    }
    
    [UseFirstOrDefault]
    [UseProjection]
    public static async Task<IQueryable<Contracts.Models.Manga>> GetMangaByIdAsync(
        int id,
        [Service] IMediator mediator) =>
        (await mediator.Send(new GetManga())).Where(x => x.Id == id);
}