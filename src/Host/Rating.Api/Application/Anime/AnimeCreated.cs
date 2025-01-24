using Core.Clasifiers;
using MediatR;

//Pay attention masstransit contracts should have same namespace.
// ReSharper disable once CheckNamespace
namespace Anime.Contracts.Models.Events;

public record AnimeCreated(
    int Id,
    Demographics Demographics,
    DateTimeOffset AddedAt) : INotification;