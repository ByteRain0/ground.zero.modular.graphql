using Anime.Contracts.Services.Studio.Queries;
using Anime.Service.Infrastructure.Data;
using HotChocolate.Pagination;
using MediatR;

namespace Anime.Service.Application.Studio.QueryHandlers;

internal class GetStudiosHandler(AnimeDbContext dbContext)
    : IRequestHandler<GetStudios, Page<Contracts.Models.Studio>>
{
    public async Task<Page<Contracts.Models.Studio>> Handle(
        GetStudios request,
        CancellationToken cancellationToken)
        => await dbContext
            .Studios
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Id)
            .ToPageAsync(request.PagingArguments, cancellationToken);
}