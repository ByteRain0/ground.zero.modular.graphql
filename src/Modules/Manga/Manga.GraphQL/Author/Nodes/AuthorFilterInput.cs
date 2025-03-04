using Core.QueryFilters;
using HotChocolate.Data.Filters;

namespace Manga.GraphQL.Author.Nodes;

public class AuthorFilterInput : FilterInputType<Contracts.Models.Author>
{
    protected override void Configure(IFilterInputTypeDescriptor<Contracts.Models.Author> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(f => f.Name)
            // Define the custom filtering we want to add to a specific field.
            // Can be set up for a type from a centralized location as well.
            .Type<SearchStringOperationFilterInputType>();
    }
}