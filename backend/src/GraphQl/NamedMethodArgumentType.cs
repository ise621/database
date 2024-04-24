using Database.Data;
using HotChocolate.Types;

namespace Database.GraphQl;

public sealed class NamedMethodArgumentType
    : ObjectType<NamedMethodArgument>
{
    protected override void Configure(
        IObjectTypeDescriptor<NamedMethodArgument> descriptor
    )
    {
        // Note that in the GraphQL configuration we map the scalar "Any"
        // to `JsonType`.
        descriptor
            .Field(x => x.Value)
            .Type<NonNullType<JsonType>>()
            .Resolve(context =>
                context.Parent<NamedMethodArgument>()
                    .Value
                    .RootElement
            );
    }
}