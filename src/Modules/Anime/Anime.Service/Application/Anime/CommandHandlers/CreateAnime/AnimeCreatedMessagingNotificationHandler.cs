using Anime.Contracts.Models.Events;
using Anime.Contracts.Services.Anime.Events;
using Anime.Contracts.Services.Anime.Telemetry;
using Core.Messaging;
using Core.Otel;
using Core.Otel.Sources;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Anime.Service.Application.Anime.CommandHandlers.CreateAnime;

public class AnimeCreatedMessagingNotificationHandler : INotificationHandler<AnimeCreated>
{
    private readonly IMessageSender _messageSender;
    private readonly ILogger<AnimeCreatedMessagingNotificationHandler> _logger;

    public AnimeCreatedMessagingNotificationHandler(
        IMessageSender messageSender,
        ILogger<AnimeCreatedMessagingNotificationHandler> logger)
    {
        _messageSender = messageSender;
        _logger = logger;
    }

    public async Task Handle(AnimeCreated notification, CancellationToken cancellationToken)
    {
        using var notificationActivity = JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity("Push notification to rabbitmq");
        notificationActivity?.AddTag(AnimeTelemetryTags.Topic, AnimeTopicNames.AnimeAddedTopicName);
        notificationActivity?.AddTag(AnimeTelemetryTags.AnimeId, notification.Id);
        notificationActivity?.SetTag("event", "anime:created");

        try
        {
            await _messageSender.PublishMessageAsync(notification, cancellationToken);
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