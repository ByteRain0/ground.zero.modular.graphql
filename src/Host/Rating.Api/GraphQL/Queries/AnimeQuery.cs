using HotChocolate.Fusion.SourceSchema.Types;
using Rating.Api.Domain;

namespace Rating.Api.GraphQL.Queries;

[QueryType]
public static class AnimeQuery
{
    [Lookup]
    [NodeResolver]
    [Internal]
    public static Domain.Anime GetAnimeById(int id)
    {
        return new()
        {
            Id = id
        };
    }
}