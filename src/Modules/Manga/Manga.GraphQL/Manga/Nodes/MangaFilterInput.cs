using Core.QueryFilters;
using HotChocolate.Data.Filters;

namespace Manga.GraphQL.Manga.Nodes;

public class MangaFilterInput: FilterInputType<Contracts.Models.Manga>
{
    protected override void Configure(IFilterInputTypeDescriptor<Contracts.Models.Manga> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(f => f.Title)
            .Type<SearchStringOperationFilterInputType>();
    }
}