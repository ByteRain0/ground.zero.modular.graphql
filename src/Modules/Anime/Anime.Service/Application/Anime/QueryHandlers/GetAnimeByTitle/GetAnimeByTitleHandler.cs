using System.Diagnostics;
using Anime.Contracts.Services.Anime.Telemetry;
using Anime.Service.Infrastructure.Data.DataLoaders;
using MediatR;

namespace Anime.Service.Application.Anime.QueryHandlers.GetAnimeByTitle;

internal class GetAnimeByTitleHandler(IAnimeByTitleDataLoader dataLoader) 
    : IRequestHandler<Contracts.Services.Anime.Queries.GetAnimeByTitle, Contracts.Models.Anime?>
{
    public async Task<Contracts.Models.Anime?> Handle(
        Contracts.Services.Anime.Queries.GetAnimeByTitle request,
        CancellationToken cancellationToken)
    {
        Activity.Current?.SetTag(AnimeTelemetryTags.AnimeIdOrTitle,request.Title);
        return await dataLoader.LoadAsync(request.Title, cancellationToken);
    }
}

