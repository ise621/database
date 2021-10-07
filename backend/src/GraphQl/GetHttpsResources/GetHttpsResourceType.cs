using HotChocolate.Types;

namespace Database.GraphQl.GetHttpsResources
{
    public sealed class GetHttpsResourceType
      : EntityType<Data.GetHttpsResource, GetHttpsResourceByIdDataLoader>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Data.GetHttpsResource> descriptor
            )
        {
            base.Configure(descriptor);
            descriptor.BindFieldsExplicitly();
        }
    }
}
