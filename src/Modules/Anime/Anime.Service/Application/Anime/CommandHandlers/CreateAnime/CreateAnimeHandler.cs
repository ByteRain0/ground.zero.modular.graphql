using System.Diagnostics;
using Anime.Contracts.Models.Events;
using Anime.Contracts.Services.Anime.Telemetry;
using Anime.Service.Infrastructure.Data;
using Core.Otel;
using Core.Otel.Sources;
using FluentValidation;
using FluentValidation.Results;
using HotChocolate.Subscriptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Anime.Service.Application.Anime.CommandHandlers.CreateAnime;

internal class CreateAnimeHandler
    : IRequestHandler<Contracts.Services.Anime.Commands.CreateAnime>
{
    private readonly AnimeDbContext _animeDbContext;
    private readonly TimeProvider _timeProvider;
    private readonly ILogger<CreateAnimeHandler> _logger;
    private readonly IMediator _mediator;

    public CreateAnimeHandler(
        AnimeDbContext animeDbContext,
        ITopicEventSender topicEventSender,
        TimeProvider timeProvider,
        ILogger<CreateAnimeHandler> logger, IMediator mediator)
    {
        _animeDbContext = animeDbContext;
        _timeProvider = timeProvider;
        _logger = logger;
        _mediator = mediator;
    }
    
    public async Task Handle(
        Contracts.Services.Anime.Commands.CreateAnime request,
        CancellationToken cancellationToken)
    {
        using (JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity("Check if anime with provided title exists"))
        {
            Activity.Current?.SetTag(AnimeTelemetryTags.AnimeTitle, request.Title);
            
            var animeExists = await _animeDbContext.Animes.AnyAsync(
                x => x.Title.ToLower() == request.Title.ToLower(), cancellationToken);

            if (animeExists)
            {
                var failure = new ValidationFailure(
                    propertyName: nameof(Contracts.Services.Anime.Commands.CreateAnime.Title),
                    errorMessage: "An entity with the provided title already exists",
                    attemptedValue: request.Title);

                var validationException = new ValidationException([failure]);
                Activity.Current?.AddExceptionAndFail(validationException);
                throw validationException;
            }
        }

        using var saveAnimeToDbActivity = JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity("Save anime to database");

        var anime = new Contracts.Models.Anime
        {
            Title = request.Title,
            StudioId = request.StudioId,
            ReleaseDate = request.ReleaseDate,
            Synopsis = request.Synopsis,
            Demographics = request.Demographics,
            TotalEpisodes = request.TotalEpisodes,
            IsCompleted = false,
            IsAiring = false
        };
        await _animeDbContext.Animes.AddAsync(anime, cancellationToken);
        
        try
        {
            await _animeDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            saveAnimeToDbActivity?.AddExceptionAndFail(exception);
            throw;
        }
        saveAnimeToDbActivity?.Stop();

        _logger.LogInformation("New anime created. Title {Title}", anime.Title);
        
        using var publishNotificationActivity =
            JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity("Publish event");
        
        await _mediator.Publish(new AnimeCreated(
            anime.Id,
            anime.Demographics,
            _timeProvider.GetUtcNow()), cancellationToken);
    }
}