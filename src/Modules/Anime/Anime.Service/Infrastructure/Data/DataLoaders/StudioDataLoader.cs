using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Anime.Service.Infrastructure.Data.DataLoaders;

internal sealed class StudioDataLoader
{
    /// <summary>
    /// Can be used when we query for multiple anime and want in some cases also studio data.
    /// Theoretically this would result in less pressure on db... but still runs 2 queries for data retrieval.
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, Contracts.Models.Studio>>
        GetStudioByIdAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken,
            AnimeDbContext context) =>
        await context
            .Studios
            .Where(st => keys.Contains(st.Id))
            .ToDictionaryAsync(st => st.Id, cancellationToken);
}