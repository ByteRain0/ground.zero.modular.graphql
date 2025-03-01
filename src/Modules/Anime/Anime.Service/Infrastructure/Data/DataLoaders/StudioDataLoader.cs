using GreenDonut;
using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;
using static Anime.Service.Infrastructure.Data.Configurations.AnimeOrderingConfiguration;

namespace Anime.Service.Infrastructure.Data.DataLoaders;

internal sealed class StudioDataLoader
{
    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, Contracts.Models.Studio>>
        GetStudioByIdAsync(
            IReadOnlyList<int> keys,
            QueryContext<Contracts.Models.Studio> queryContext,
            CancellationToken cancellationToken,
            AnimeDbContext context) =>
        await context
            .Studios
            .With(queryContext)
            .Where(st => keys.Contains(st.Id))
            .ToDictionaryAsync(st => st.Id, cancellationToken);

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