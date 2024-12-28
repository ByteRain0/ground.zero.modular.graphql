using HotChocolate.Data.Filters;
using HotChocolate.Types;

namespace Core.QueryFilters;

/// <summary>
/// Custom filter configuration for string based fields.
/// Defines the list of available filtering options. ex: Database first approach.
/// </summary>
public class RestrictedStringOperationFilterInputType : StringOperationFilterInputType
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        descriptor.Operation(DefaultFilterOperations.Equals).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.NotEquals).Type<StringType>();
    }
}