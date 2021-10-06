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
            // descriptor.Field(x => x.Id);
            // descriptor.Field(x => x.Description);
            // descriptor.Field(x => x.HashValue);
            // descriptor.Field(x => x.Locator);
            // descriptor.Field(x => x.FormatId);
        }
    }
}
