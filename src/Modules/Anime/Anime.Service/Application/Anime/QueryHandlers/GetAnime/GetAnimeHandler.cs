using Anime.Service.Infrastructure.Data;
using Core.Otel.Sources;
using GreenDonut.Data;
using MediatR;
using static Anime.Service.Infrastructure.Data.Configurations.AnimeOrderingConfiguration;

namespace Anime.Service.Application.Anime.QueryHandlers.GetAnime;

internal class GetAnimeQueryHandler(AnimeDbContext animeDbContext)
    : IRequestHandler<Contracts.Services.Anime.Queries.GetAnime, Page<Contracts.Models.Anime>>
{
    public async Task<Page<Contracts.Models.Anime>> Handle(
        Contracts.Services.Anime.Queries.GetAnime request,
        CancellationToken cancellationToken)
    {
        using var activity = JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity("Retrieve anime from database");

        return await animeDbContext
            .Animes
            .With(request.QueryContext, DefaultAnimeOrder)
            .ToPageAsync(request.PagingArguments, cancellationToken: cancellationToken);
    }
}

