using Anime.Service.Infrastructure.Data;
using HotChocolate.Pagination;
using MediatR;

namespace Anime.Service.Application.Anime.QueryHandlers.GetAnime;

internal class GetAnimeQueryHandler(AnimeDbContext animeDbContext) 
    : IRequestHandler<Contracts.Services.Anime.Queries.GetAnime, Page<Contracts.Models.Anime>>
{
    public async Task<Page<Contracts.Models.Anime>> Handle(
        Contracts.Services.Anime.Queries.GetAnime request,
        CancellationToken cancellationToken) =>
        await animeDbContext
            .Animes
            // Filter for Studio                
            .Where(x => 
                !request.QueryFilters.StudioId.HasValue 
                || x.StudioId == request.QueryFilters.StudioId)
            // Filter by Title
            .Where(x => 
                string.IsNullOrEmpty(request.QueryFilters.Title) 
                || x.Title.ToLower().Contains(request.QueryFilters.Title.ToLower()))
            // Filter by IsAiring
            .Where(x => 
                !request.QueryFilters.IsAiring.HasValue
                || x.IsAiring == request.QueryFilters.IsAiring)
            // Filter by IsCompleted
            .Where(x => 
                !request.QueryFilters.IsCompleted.HasValue
                || x.IsCompleted == request.QueryFilters.IsCompleted)
            .OrderBy(x => x.Title)
            .ThenBy(x => x.Id)
            .ToPageAsync(request.PagingArguments, cancellationToken);
}