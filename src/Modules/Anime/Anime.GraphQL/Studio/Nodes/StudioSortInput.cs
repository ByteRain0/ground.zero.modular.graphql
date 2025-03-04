using HotChocolate.Data.Sorting;

namespace Anime.GraphQL.Studio.Nodes;

public class StudioSortInput : SortInputType<Contracts.Models.Studio>
{
    protected override void Configure(ISortInputTypeDescriptor<Contracts.Models.Studio> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Name);
    }
}