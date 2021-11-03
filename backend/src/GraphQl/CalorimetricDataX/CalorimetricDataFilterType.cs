using Database.GraphQl.DataX;
using HotChocolate.Data.Filters;

namespace Database.GraphQl.CalorimetricDataX
{
    public sealed class CalorimetricDataFilterType
      : DataFilterTypeBase<Data.CalorimetricData>
    {
        protected override void Configure(
          IFilterInputTypeDescriptor<Data.CalorimetricData> descriptor
          )
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.GValues);
            descriptor.Field(x => x.UValues);
        }
    }
}
