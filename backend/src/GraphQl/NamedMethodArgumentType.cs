using HotChocolate.Types;

namespace Database.GraphQl
{
    public sealed class NamedMethodArgumentType
      : ObjectType<Data.NamedMethodArgument>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Data.NamedMethodArgument> descriptor
            )
        {
            base.Configure(descriptor);
            // TODO Use `AnyType` for `Value` and use `ToNestedCollections` from backup of metabase backend. See also https://github.com/ChilliCream/hotchocolate/issues/3661 and https://github.com/ChilliCream/hotchocolate/pull/3713
            descriptor.Ignore(x => x.Value);
        }
    }
}
