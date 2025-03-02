using GreenDonut;
using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;
using static Anime.Service.Infrastructure.Data.Configurations.AnimeOrderingConfiguration;

namespace Anime.Service.Infrastructure.Data.DataLoaders;

internal sealed class AnimeDataLoader
{
    [DataLoader]
    internal static async Task<Dictionary<int, Contracts.Models.Anime>>
        GetAnimeByIdDataLoader(
            IReadOnlyList<int> keys,
            QueryContext<Contracts.Models.Anime> queryContext,
            CancellationToken cancellationToken,
            AnimeDbContext context) =>
        await context
            .Animes
            .With(queryContext)
            .Where(an => keys.Contains(an.Id))
            .ToDictionaryAsync(an => an.Id, cancellationToken);


    [DataLoader]
    internal static async Task<Dictionary<string, Contracts.Models.Anime>>
        GetAnimeByTitleDataLoader(
            IReadOnlyList<string> keys,
            QueryContext<Contracts.Models.Anime> queryContext,
            CancellationToken cancellationToken,
            AnimeDbContext context) =>
        await context
            .Animes
            .With(queryContext)
            .Where(an => keys.Select(x => x.ToLower()).Contains(an.Title.ToLower()))
            // The key that gets added here should be from the keys list,
            // if it gets modified the data loader cannot map the result.
            .ToDictionaryAsync(an => an.Title, cancellationToken);


    [DataLoader]
    internal static async Task<Dictionary<int, Page<Contracts.Models.Anime>>>
        GetAnimeByStudioIdAsync(
            IReadOnlyList<int> studioIds,
            PagingArguments pagingArguments,
            QueryContext<Contracts.Models.Anime> queryContext,
            CancellationToken cancellationToken,
            AnimeDbContext context) =>
        await context.Animes
            .With(queryContext, DefaultAnimeOrder)
            .Where(x => studioIds.Contains(x.StudioId))
            .ToBatchPageAsync(x => x.StudioId, pagingArguments, cancellationToken);
}