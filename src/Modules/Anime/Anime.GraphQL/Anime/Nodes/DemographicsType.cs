using Core.Clasifiers;

namespace Anime.GraphQL.Anime.Nodes;

public class DemographicsType : EnumType<Demographics>
{
    protected override void Configure(IEnumTypeDescriptor<Demographics> descriptor)
    {
        descriptor.BindValues(BindingBehavior.Implicit);
    }
}