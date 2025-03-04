using Anime.Contracts.Models.Events;
using Anime.Contracts.Services.Anime.Events;
using Anime.Contracts.Services.Anime.Telemetry;
using Core.Otel;
using Core.Otel.Sources;
using HotChocolate.Subscriptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Anime.Service.Application.Anime.CommandHandlers.CreateAnime;

/// <summary>
/// Pushes notification to GraphQl SSE subscribers.
/// TODO: write a unit test that will ensure that the handler is writing the correct log message for monitoring purposes.
/// </summary>
public class AnimeCreateGraphqlNotificationHandler : INotificationHandler<AnimeCreated>
{
    private readonly ITopicEventSender _topicEventSender;
    private readonly ILogger<AnimeCreateGraphqlNotificationHandler> _logger;

    public AnimeCreateGraphqlNotificationHandler(ITopicEventSender topicEventSender,
        ILogger<AnimeCreateGraphqlNotificationHandler> logger)
    {
        _topicEventSender = topicEventSender;
        _logger = logger;
    }

    public async Task Handle(AnimeCreated notification, CancellationToken cancellationToken)
    {
        using var notificationActivity = JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity("Push notification to graphql topic");
        notificationActivity?.AddTag(AnimeTelemetryTags.Topic, AnimeTopicNames.AnimeAddedTopicName);
        notificationActivity?.AddTag(AnimeTelemetryTags.AnimeId, notification.Id);
        notificationActivity?.SetTag("event", "anime:created");

        try
        {
            await _topicEventSender.SendAsync(
                AnimeTopicNames.AnimeAddedTopicName,
                notification,
                cancellationToken);
        }
        // TODO: Monitor the behavior of the subscription provider.
        // In case of repeated failures consider implementing an outbox setup.
        catch (Exception ex)
        {
            notificationActivity?.AddExceptionAndFail(ex);

            _logger.LogError(
                exception: ex,
                message: "Issue sending notification. {NotificationType} {AnimeId}",
                typeof(AnimeCreated), notification.Id);

            // TODO: decide if we want to stop the flow in case of failure or continue.
            // throw;
        }
    }
}