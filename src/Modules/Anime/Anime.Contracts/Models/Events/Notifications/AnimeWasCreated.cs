using Core.Clasifiers;
using MediatR;

namespace Anime.Contracts.Models.Events.Notifications;

public record AnimeWasCreated(
    int Id,
    Demographics Demographics,
    DateTimeOffset AddedAt) : INotification;