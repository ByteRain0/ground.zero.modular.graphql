namespace Anime.GraphQL.Anime.Nodes;

/// <summary>
/// Re-write the graph node / model that is exposed to the client application.
/// </summary>
[ObjectType<Contracts.Models.Anime>]
public static partial class AnimeNode
{
    static partial void Configure(IObjectTypeDescriptor<Contracts.Models.Anime> descriptor)
    {
        descriptor.BindFieldsImplicitly();
    }

    public static int InternalId([Parent] Contracts.Models.Anime anime) => anime.Id;
}