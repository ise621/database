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
            descriptor
                .Field(x => x.ParentId)
                .Ignore();
            descriptor
                .Field(x => x.Parent)
                .ResolveWith<GetHttpsResourceResolvers>(t => t.GetParent(default!, default!, default!));
            descriptor
                .Field(x => x.Children)
                .ResolveWith<GetHttpsResourceResolvers>(t => t.GetChildren(default!, default!, default!));
            descriptor
                .Field(x => x.DataId)
                .Ignore();
            descriptor
                .Field(x => x.Data)
                .ResolveWith<GetHttpsResourceResolvers>(t => t.GetData(default!, default!, default!));
        }
    }
}
