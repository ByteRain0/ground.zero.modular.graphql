namespace Manga.GraphQL.Manga.Nodes;

[ObjectType<Contracts.Models.Manga>]
public static partial class MangaNode
{
    static partial void Configure(IObjectTypeDescriptor<Contracts.Models.Manga> descriptor)
    {
        descriptor.BindFieldsImplicitly();

        descriptor
            .Field(x => x.Id);

        descriptor
            .Field(x => x.Title)
            .UseFiltering();

        descriptor
            .Field(x => x.AuthorId)
            .Ignore();

        descriptor
            .Field(x => x.Demographics)
            .Ignore();
    }
}