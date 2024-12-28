namespace Manga.GraphQL.Author.Nodes;

[ObjectType<Contracts.Models.Author>]
public static partial class AuthorNode
{
    static partial void Configure(IObjectTypeDescriptor<Contracts.Models.Author> descriptor)
    {
        descriptor.BindFieldsImplicitly();

        descriptor.Field(x => x.Id)
            .Ignore();

        descriptor.Field(x => x.Mangas)
            .Ignore();
    }
}