using Anime.Contracts.Models.Events.Notifications;
using Anime.Contracts.Services.Anime.Events;
using Anime.Contracts.Services.Anime.Telemetry;
using Core.Otel;
using HotChocolate.Subscriptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Anime.Service.Application.Anime.CommandHandlers.CreateAnime;

/// <summary>
/// Pushes notification to GraphQl SSE subscribers.
/// TODO: write a unit test that will ensure that the handler is writing the correct log message for monitoring purposes.
/// </summary>
public class AnimeWasCreateNotificationHandler : INotificationHandler<AnimeWasCreated>
{
    private readonly ITopicEventSender _topicEventSender;
    private readonly ILogger<AnimeWasCreateNotificationHandler> _logger;

    public AnimeWasCreateNotificationHandler(ITopicEventSender topicEventSender,
        ILogger<AnimeWasCreateNotificationHandler> logger)
    {
        _topicEventSender = topicEventSender;
        _logger = logger;
    }

    public async Task Handle(AnimeWasCreated notification, CancellationToken cancellationToken)
    {
        using var notificationActivity = RunTimeDiagnosticConfig.Source.StartActivity("Push notification to topic");
        notificationActivity?.AddTag(AnimeTelemetryTags.Topic, AnimeTopicNames.AnimeAddedTopicName);
        notificationActivity?.AddTag(AnimeTelemetryTags.AnimeId, notification.Id);
        
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
                typeof(AnimeWasCreated), notification.Id);
            
            // TODO: decide if we want to stop the flow in case of failure or continue.
            // throw;
        }
    }
}