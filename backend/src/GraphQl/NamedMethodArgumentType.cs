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
            // Note that in the GraphQL configuration we map the scalar "Any"
            // to `JsonType`.
            descriptor
                .Field(x => x.Value)
                .Type<NonNullType<JsonType>>()
                .Resolve(context =>
                    context.Parent<Data.NamedMethodArgument>()
                    .Value
                    .RootElement
                );
        }
    }
}
