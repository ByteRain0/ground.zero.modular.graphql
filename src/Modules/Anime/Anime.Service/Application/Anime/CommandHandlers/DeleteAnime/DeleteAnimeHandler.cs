using Anime.Service.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anime.Service.Application.Anime.CommandHandlers.DeleteAnime;

internal class DeleteAnimeHandler
    : IRequestHandler<Contracts.Services.Anime.Commands.DeleteAnime>
{
    private readonly AnimeDbContext _animeDbContext;

    public DeleteAnimeHandler(AnimeDbContext animeDbContext)
    {
        _animeDbContext = animeDbContext;
    }

    public async Task Handle(
        Contracts.Services.Anime.Commands.DeleteAnime request,
        CancellationToken cancellationToken)
    {
        await _animeDbContext
            .Animes
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}