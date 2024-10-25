using HotChocolate.Types;
using Database.Data;

namespace Database.GraphQl.DataX;

public sealed class DataType
    : InterfaceType<IData>
{
    protected override void Configure(
        IInterfaceTypeDescriptor<IData> descriptor
    )
    {
        // `1..` is a range as introduced in https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#indices-and-ranges
        descriptor.Name(nameof(IData)[1..]);
        descriptor
            .Field("uuid")
            .Type<NonNullType<UuidType>>();
        descriptor
            .Field("timestamp")
            .Type<NonNullType<DateTimeType>>();
        descriptor
            .Field("resourceTree")
            .Type<NonNullType<ObjectType<GetHttpsResourceTree>>>();
        descriptor
            .Field(x => x.Locale)
            .Type<NonNullType<LocaleType>>();
    }
}