using GreenDonut.Data;

namespace Anime.Service.Infrastructure.Data.Configurations;

internal static class AnimeOrderingConfiguration
{
    public static SortDefinition<Contracts.Models.Anime> DefaultAnimeOrder(
        SortDefinition<Contracts.Models.Anime> sortDefinition)
        => sortDefinition
            .IfEmpty(x => x.AddAscending(an => an.Title))
            .AddAscending(an => an.Id);
}