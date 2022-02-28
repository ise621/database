using Database.GraphQl.DataX;
using HotChocolate.Data.Filters;

namespace Database.GraphQl.PhotovoltaicDataX
{
    public sealed class PhotovoltaicDataFilterType
      : DataFilterTypeBase<Data.PhotovoltaicData>
    {
        protected override void Configure(
          IFilterInputTypeDescriptor<Data.PhotovoltaicData> descriptor
          )
        {
            base.Configure(descriptor);
            descriptor.Field(x => x.Id);
        }
    }
}
