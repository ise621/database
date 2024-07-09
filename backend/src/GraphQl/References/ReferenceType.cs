using Database.Data;
using HotChocolate.Types;

namespace Database.GraphQl.References;

public sealed class ReferenceType
    : InterfaceType<IReference>
{
    protected override void Configure(IInterfaceTypeDescriptor<IReference> descriptor)
    {
        descriptor.Name(nameof(IReference)[1..]);
    }
}