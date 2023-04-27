using HotChocolate.Types;

namespace Database.GraphQl.Users
{
    public sealed class UserType
      : EntityType<Data.User, UserByIdDataLoader>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Data.User> descriptor
            )
        {
            base.Configure(descriptor);
        }
    }
}
