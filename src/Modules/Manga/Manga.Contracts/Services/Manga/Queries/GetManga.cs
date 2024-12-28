using MediatR;

namespace Manga.Contracts.Services.Manga.Queries;

public record GetManga: IRequest<IQueryable<Models.Manga>>;