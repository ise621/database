using HotChocolate.Data.Filters;

namespace Database.GraphQl
{
    public sealed class NamedMethodArgumentFilterType
      : FilterInputType<Data.NamedMethodArgument>
    {
        protected override void Configure(
          IFilterInputTypeDescriptor<Data.NamedMethodArgument> descriptor
          )
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Name);
            // TODO descriptor.Field(x => x.Value);
        }
    }
}
