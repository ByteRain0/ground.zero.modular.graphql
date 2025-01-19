using Core.Clasifiers;
using MediatR;

namespace Anime.Contracts.Models.Events;

public record AnimeCreated(
    int Id,
    Demographics Demographics,
    DateTimeOffset AddedAt) : INotification;