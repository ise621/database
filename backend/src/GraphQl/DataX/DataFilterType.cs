using HotChocolate.Data.Filters;

namespace Database.GraphQl.DataX
{
    public class DataFilterType
      : DataFilterTypeBase<Data.IData>
    {
        protected override void Configure(
          IFilterInputTypeDescriptor<Data.IData> descriptor
          )
        {
            base.Configure(descriptor);
        }
    }
}
