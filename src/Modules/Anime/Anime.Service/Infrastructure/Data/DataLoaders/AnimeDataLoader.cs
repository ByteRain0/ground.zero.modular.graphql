using GreenDonut;
using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;

namespace Anime.Service.Infrastructure.Data.DataLoaders;

internal sealed class AnimeDataLoader
{
    [DataLoader]
    internal static async Task<Dictionary<int, Contracts.Models.Anime>>
        GetAnimeByIdDataLoader(
            IReadOnlyList<int> keys,
            QueryContext<Contracts.Models.Anime> query,
            CancellationToken cancellationToken,
            AnimeDbContext context)
        => await context
            .Animes
            .With(query)
            .Where(an => keys.Contains(an.Id))
            .ToDictionaryAsync(an => an.Id, cancellationToken);
    
    [DataLoader]
    internal static async Task<Dictionary<string, Contracts.Models.Anime>>
        GetAnimeByTitleDataLoader(
            IReadOnlyList<string> keys,
            QueryContext<Contracts.Models.Anime> query,
            CancellationToken cancellationToken,
            AnimeDbContext context) =>
        await context
            .Animes
            .With(query)
            .Where(an => keys.Select(x => x.ToLower()).Contains(an.Title.ToLower()))
            // The key that gets added here should be from the keys list,
            // if it gets modified the dataloader cannot map the result.
            .ToDictionaryAsync(an => an.Title, cancellationToken); 
    
}