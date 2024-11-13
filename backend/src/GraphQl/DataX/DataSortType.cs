using Database.Data;
using HotChocolate.Data.Sorting;

namespace Database.GraphQl.DataX;

public class DataSortType
    : DataSortTypeBase<IData>
{
    protected override void Configure(
        ISortInputTypeDescriptor<IData> descriptor
    )
    {
        base.Configure(descriptor);
    }
}