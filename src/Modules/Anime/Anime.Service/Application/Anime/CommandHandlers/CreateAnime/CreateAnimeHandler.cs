using System.Diagnostics;
using Anime.Contracts.Models.Events.Notifications;
using Anime.Service.Infrastructure.Data;
using Core.Otel;
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
        using (RunTimeDiagnosticConfig.Source.StartActivity("Check if anime with provided title exists"))
        {
            var animeExists = await _animeDbContext.Animes.AnyAsync(
                x => x.Title.ToLower() == request.Title.ToLower(), cancellationToken);

            if (animeExists)
            {
                Activity.Current?.SetTag("AnimeTitle", request.Title);

                var failure = new ValidationFailure(
                    propertyName: nameof(Contracts.Services.Anime.Commands.CreateAnime.Title),
                    errorMessage: "An entity with the provided title already exists",
                    attemptedValue: request.Title);

                throw new ValidationException([failure]);
            }
        }

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

        using (RunTimeDiagnosticConfig.Source.StartActivity("Save anime to database"))
        {
            await _animeDbContext.Animes.AddAsync(anime, cancellationToken);
            await _animeDbContext.SaveChangesAsync(cancellationToken);
        }

        _logger.LogInformation("New anime created. Title {Title}", anime.Title);

        await _mediator.Publish(new AnimeWasCreated(
            anime.Id,
            anime.Demographics,
            _timeProvider.GetUtcNow()), cancellationToken);
    }
}