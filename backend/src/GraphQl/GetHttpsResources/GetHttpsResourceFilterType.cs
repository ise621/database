using HotChocolate.Data.Filters;

namespace Database.GraphQl.GetHttpsResources
{
    public sealed class GetHttpsResourceFilterType
      : FilterInputType<Data.GetHttpsResource>
    {
        protected override void Configure(
          IFilterInputTypeDescriptor<Data.GetHttpsResource> descriptor
          )
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.Description);
            descriptor.Field(x => x.HashValue);
            descriptor.Field(x => x.DataFormatId);
            descriptor.Field(x => x.AppliedConversionMethod);
            descriptor.Field(x => x.ArchivedFilesMetaInformation);
            descriptor.Field(x => x.DataId);
            descriptor.Field(x => x.ParentId);
            descriptor.Field(x => x.Parent);
        }
    }
}
