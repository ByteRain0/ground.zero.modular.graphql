using System.Diagnostics;
using Anime.Contracts.Services.Anime.Telemetry;
using Anime.Service.Infrastructure.Data.DataLoaders;
using GreenDonut.Data;
using MediatR;

namespace Anime.Service.Application.Anime.QueryHandlers.GetAnimeById;

internal class GetAnimeByIdHandler(IAnimeByIdDataLoader dataLoader) 
    : IRequestHandler<Contracts.Services.Anime.Queries.GetAnimeById, Contracts.Models.Anime?>
{
    public async Task<Contracts.Models.Anime?> Handle(
        Contracts.Services.Anime.Queries.GetAnimeById request,
        CancellationToken cancellationToken)
    {
        Activity.Current?.SetTag(AnimeTelemetryTags.AnimeId,request.Id);
        return await dataLoader
            .LoadAsync(request.Id, cancellationToken);
    }
}

