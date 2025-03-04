using System.Diagnostics;
using Anime.Contracts.Models.Events;
using MassTransit;
using Rating.Api.Infrastructure;

namespace Rating.Api.Application.Anime;

public class AnimeCreatedConsumer : IConsumer<AnimeCreated>
{
    private readonly DatabaseMock _databaseMock;

    public AnimeCreatedConsumer(DatabaseMock databaseMock)
    {
        _databaseMock = databaseMock;
    }

    public Task Consume(ConsumeContext<AnimeCreated> context)
    {
        using var consumeEventActivity = RatingApiRunTimeDiagnosticConfig.Source.StartActivity("Handling anime:created event");
        Activity.Current?.AddTag("anime_id", context.Message.Id);

        _databaseMock.Ratings.Add(new Domain.Rating()
        {
            Id = Guid.NewGuid(),
            EntityType = "Anime",
            EntityId = context.Message.Id,
            Mark = new Random().NextDouble() * 10
        });

        return Task.CompletedTask;
    }
}