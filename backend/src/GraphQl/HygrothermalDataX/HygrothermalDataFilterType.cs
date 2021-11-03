using Database.GraphQl.DataX;
using HotChocolate.Data.Filters;

namespace Database.GraphQl.HygrothermalDataX
{
    public sealed class HygrothermalDataFilterType
      : DataFilterTypeBase<Data.HygrothermalData>
    {
        protected override void Configure(
          IFilterInputTypeDescriptor<Data.HygrothermalData> descriptor
          )
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Id);
        }
    }
}
