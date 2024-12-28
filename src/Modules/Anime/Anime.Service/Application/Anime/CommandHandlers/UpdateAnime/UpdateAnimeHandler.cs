using System.Diagnostics;
using Anime.Contracts.Exceptions;
using Anime.Contracts.Services.Anime.Telemetry;
using Anime.Service.Infrastructure.Data;
using Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anime.Service.Application.Anime.CommandHandlers.UpdateAnime;

internal class UpdateAnimeHandler 
    : IRequestHandler<Contracts.Services.Anime.Commands.UpdateAnime>
{
    private readonly AnimeDbContext _animeDbContext;

    public UpdateAnimeHandler(AnimeDbContext animeDbContext)
    {
        _animeDbContext = animeDbContext;
    }

    public async Task Handle(
        Contracts.Services.Anime.Commands.UpdateAnime request, 
        CancellationToken cancellationToken)
    {
        Activity.Current?.SetTag(AnimeTelemetryTags.AnimeIdOrTitle, request.Id);

        var anime = await _animeDbContext
            .Animes
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (anime is null)
        {
            var exception = new AnimeNotFoundException(request.Id.ToString());
            Activity.Current?.AddException(exception);
            Activity.Current?.SetStatus(ActivityStatusCode.Error);
            throw exception;
        }
        
        anime.Title.UpdateIfHasValue(
            request.Title,
            title => anime.Title = title,
            () => Activity.Current?.AddEvent(new ActivityEvent("AnimeTitleUpdated"))
        );

        anime.StudioId.UpdateIfHasValue(
            request.StudioId,
            studioId => anime.StudioId = studioId,
            () => Activity.Current?.AddEvent(new ActivityEvent("AnimeStudioUpdated"))
        );

        anime.ReleaseDate.UpdateIfHasValue(
            request.ReleaseDate,
            releaseDate => anime.ReleaseDate = releaseDate,
            () => Activity.Current?.AddEvent(new ActivityEvent("AnimeReleaseDateUpdated"))
        );

        anime.Synopsis.UpdateIfHasValue(
            request.Synopsis,
            synopsis => anime.Synopsis = synopsis,
            () => Activity.Current?.AddEvent(new ActivityEvent("AnimeSynopsisUpdated"))
        );

        anime.Demographics.UpdateIfHasValue(
            request.Demographics,
            demographics => anime.Demographics = demographics,
            () => Activity.Current?.AddEvent(new ActivityEvent("AnimeDemographicsUpdated"))
        );

        anime.TotalEpisodes.UpdateIfHasValue(
            request.TotalEpisodes,
            totalEpisodes => anime.TotalEpisodes = totalEpisodes,
            () => Activity.Current?.AddEvent(new ActivityEvent("AnimeTotalEpisodesUpdated"))
        );

        _animeDbContext.Animes.Update(anime);
        await _animeDbContext.SaveChangesAsync(cancellationToken);
    }
}