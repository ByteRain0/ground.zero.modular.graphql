using HotChocolate.Data.Sorting;

namespace Manga.GraphQL.Manga.Nodes;

public class MangaSortInput : SortInputType<Contracts.Models.Manga>
{
    protected override void Configure(ISortInputTypeDescriptor<Contracts.Models.Manga> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Title);
        descriptor.Field(x => x.ReleaseDate);
    }
}