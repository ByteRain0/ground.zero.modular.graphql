using System.Diagnostics;
using Anime.Contracts.Exceptions;
using Anime.Contracts.Services.Anime.Telemetry;
using Anime.Service.Infrastructure.Data.DataLoaders;
using MediatR;

namespace Anime.Service.Application.Anime.QueryHandlers.GetAnimeById;

internal class GetAnimeByIdHandler(IAnimeByIdDataLoader dataLoader) 
    : IRequestHandler<Contracts.Services.Anime.Queries.GetAnimeById, Contracts.Models.Anime?>
{
    public async Task<Contracts.Models.Anime?> Handle(
        Contracts.Services.Anime.Queries.GetAnimeById request,
        CancellationToken cancellationToken)
    {
        Activity.Current?.SetTag(AnimeTelemetryTags.AnimeIdOrTitle,request.Id);
        return await dataLoader.LoadAsync(request.Id, cancellationToken);
        
        // In case we want to return exact errors we can use Error patterns :
        var anime = await dataLoader.LoadAsync(request.Id, cancellationToken);

        if (anime is null)
        {
            throw new AnimeNotFoundException(idOrTitle: request.Id.ToString());
        }

        return anime;
    }
}

