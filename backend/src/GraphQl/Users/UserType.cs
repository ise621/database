using Database.Data;
using HotChocolate.Types;

namespace Database.GraphQl.Users;

public sealed class UserType
    : EntityType<User, UserByIdDataLoader>
{
    protected override void Configure(
        IObjectTypeDescriptor<User> descriptor
    )
    {
        base.Configure(descriptor);
    }
}