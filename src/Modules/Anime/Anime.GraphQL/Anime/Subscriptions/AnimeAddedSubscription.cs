using System.Runtime.CompilerServices;
using Anime.Contracts.Models.Events.Notifications;
using Anime.Contracts.Services.Anime.Events;
using Anime.Contracts.Services.Anime.Queries;
using Core.Clasifiers;
using HotChocolate.Subscriptions;
using MediatR;

namespace Anime.GraphQL.Anime.Subscriptions;

[SubscriptionType]
public static class AnimeAddedSubscription
{
    public static async IAsyncEnumerable<AnimeWasCreated> OnAnimeAddedStream(
        Demographics[]? interestedDemographics,
        ITopicEventReceiver eventReceiver,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var eventStream = await eventReceiver.SubscribeAsync<AnimeWasCreated>(
            AnimeTopicNames.AnimeAddedTopicName,
            cancellationToken);

        var demographicsSet = interestedDemographics is null 
            ? null 
            : new HashSet<Demographics>(interestedDemographics);
        
        await foreach (var message in eventStream.ReadEventsAsync().WithCancellation(cancellationToken))
        {
            if (demographicsSet?.Contains(message.Demographics) ?? true)
            {
                yield return message;
            }
        }
    }

    [Subscribe(With = nameof(OnAnimeAddedStream))]
    public static async Task<Contracts.Models.Anime?> OnAnimeAdded(
        [EventMessage] AnimeWasCreated message,
        IMediator mediator,
        CancellationToken cancellationToken) =>
        await mediator.Send(new GetAnimeById(message.Id), cancellationToken);
}