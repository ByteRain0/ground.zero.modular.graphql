using GreenDonut.Data;

namespace Anime.Service.Infrastructure.Data.Configurations;

internal static class StudioOrderingConfiguration
{
    public static SortDefinition<Contracts.Models.Studio> DefaultStudioOrder(
        SortDefinition<Contracts.Models.Studio> sortDefinition)
        => sortDefinition.IfEmpty(x => x.AddAscending(an => an.Name))
            .AddAscending(an => an.Id);
}