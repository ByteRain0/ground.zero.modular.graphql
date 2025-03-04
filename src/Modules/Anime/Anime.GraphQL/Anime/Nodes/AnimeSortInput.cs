using Anime.GraphQL.Studio.Nodes;
using HotChocolate.Data.Sorting;

namespace Anime.GraphQL.Anime.Nodes;

public class AnimeSortInput : SortInputType<Contracts.Models.Anime>
{
    protected override void Configure(ISortInputTypeDescriptor<Contracts.Models.Anime> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Title);

        descriptor.Field(x => x.TotalEpisodes);

        descriptor.Field(x => x.ReleaseDate);

        // Bind to the constraints from the studio to reuse them
        descriptor.Field(x => x.Studio)
            .Type<StudioSortInput>();
    }
}