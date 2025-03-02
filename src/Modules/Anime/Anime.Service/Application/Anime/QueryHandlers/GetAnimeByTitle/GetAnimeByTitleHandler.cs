using Anime.Contracts.Services.Anime.Telemetry;
using Anime.Service.Infrastructure.Data.DataLoaders;
using Core.Otel.Sources;
using GreenDonut.Data;
using MediatR;

namespace Anime.Service.Application.Anime.QueryHandlers.GetAnimeByTitle;

internal class GetAnimeByTitleHandler(IAnimeByTitleDataLoader dataLoader)
    : IRequestHandler<Contracts.Services.Anime.Queries.GetAnimeByTitle, Contracts.Models.Anime?>
{
    public async Task<Contracts.Models.Anime?> Handle(
        Contracts.Services.Anime.Queries.GetAnimeByTitle request,
        CancellationToken cancellationToken)
    {
        using var activity = JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity("Retrieve anime from database");
        activity?.SetTag(AnimeTelemetryTags.AnimeTitle, request.Title);

        return await dataLoader
            .With(request.QueryContext)
            .LoadAsync(request.Title, cancellationToken);
    }
}