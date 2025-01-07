using HotChocolate.Fusion.SourceSchema.Types;
using Rating.Api.Models;

namespace Rating.Api.GraphQL.Queries;

[QueryType]
public static class AnimeQuery
{
    [Lookup]
    [Internal]
    public static Anime GetAnimeById([ID<int>] int id)
    {
        return new()
        {
            Id = id
        };
    }
}