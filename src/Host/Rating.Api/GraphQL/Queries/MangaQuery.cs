using HotChocolate.Fusion.SourceSchema.Types;
using Rating.Api.Models;

namespace Rating.Api.GraphQL.Queries;

[QueryType]
public static class MangaQuery
{
    [Lookup]
    [Internal]
    public static Manga GetMangaById(int id)
    {
        return new ()
        {
            Id = id
        };
    }
}