using HotChocolate.Data.Sorting;

namespace Manga.GraphQL.Author.Nodes;

public class AuthorSortInput : SortInputType<Contracts.Models.Author>
{
    protected override void Configure(ISortInputTypeDescriptor<Contracts.Models.Author> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Name);
    }
}