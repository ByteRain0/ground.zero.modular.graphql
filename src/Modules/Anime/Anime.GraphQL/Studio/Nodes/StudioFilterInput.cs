using Core.QueryFilters;
using HotChocolate.Data.Filters;

namespace Anime.GraphQL.Studio.Nodes;

/// <summary>
/// Define what fields the app will allow filtering on.
/// </summary>
public class StudioFilterInput : FilterInputType<Contracts.Models.Studio>
{
    protected override void Configure(IFilterInputTypeDescriptor<Contracts.Models.Studio> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(f => f.Name)
            .Type<SearchStringOperationFilterInputType>();
    }
}