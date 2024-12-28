using Manga.Contracts.Services.Manga.Queries;
using Manga.Service.Infrastructure.Data;
using MediatR;

namespace Manga.Service.Application.Manga.QueryHandlers;

/// <summary>
/// Allowing graphql handle queries on top of the EF core setup.
/// Centralize query logic here, ex:
/// * Filtering based on a tenantId
/// * Filtering out the deleted entities
/// * ...
/// </summary>
/// <param name="dbContext"></param>
public class GetMangaHandler(MangaDbContext context) 
    : IRequestHandler<GetManga,IQueryable<Contracts.Models.Manga>>
{
    public Task<IQueryable<Contracts.Models.Manga>> Handle(
        GetManga request,
        CancellationToken cancellationToken)
        => Task.FromResult(context
            .Mangas
            .OrderBy(x => x.Title)
            .ThenBy(x => x.Id)
            .AsQueryable());
}