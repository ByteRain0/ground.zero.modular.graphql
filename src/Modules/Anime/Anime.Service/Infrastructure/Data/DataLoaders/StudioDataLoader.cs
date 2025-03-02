using GreenDonut;
using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;

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
}