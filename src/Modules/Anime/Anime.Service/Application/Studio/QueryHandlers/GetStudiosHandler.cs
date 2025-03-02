using Anime.Contracts.Services.Studio.Queries;
using Anime.Service.Infrastructure.Data;
using GreenDonut.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Anime.Service.Infrastructure.Data.Configurations.StudioOrderingConfiguration;

namespace Anime.Service.Application.Studio.QueryHandlers;

internal class GetStudiosHandler(AnimeDbContext dbContext)
    : IRequestHandler<GetStudios, Page<Contracts.Models.Studio>>
{
    public async Task<Page<Contracts.Models.Studio>> Handle(
        GetStudios request,
        CancellationToken cancellationToken)
        => await dbContext
            .Studios
            .With(request.QueryContext, DefaultStudioOrder)
            .ToPageAsync(request.PagingArguments, cancellationToken);
}