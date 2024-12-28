using HotChocolate.Data.Filters;
using HotChocolate.Types;

namespace Core.QueryFilters;

/// <summary>
/// Extended filtering capabilities for search based functionality.
/// Define the custom filtering we want to add to a specific field.
/// Can be set up for a type from a centralized location as well.
/// </summary>
public class SearchStringOperationFilterInputType : StringOperationFilterInputType
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        descriptor.Operation(DefaultFilterOperations.Contains).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.Equals).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.Contains).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.In).Type<StringType>();
    } 
}