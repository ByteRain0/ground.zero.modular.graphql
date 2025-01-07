using HotChocolate.Fusion.SourceSchema.Types;
using Rating.Api.Models;

namespace Rating.Api.GraphQL.Queries;

[QueryType]
public static class AnimeQuery
{
    [Lookup]
    [NodeResolver]
    [Internal]
    public static Anime GetAnimeById(int id)
    {
        return new()
        {
            Id = id
        };
    }
}