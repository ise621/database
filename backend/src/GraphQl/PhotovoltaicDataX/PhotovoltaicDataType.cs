using Database.Data;
using HotChocolate.Types;

namespace Database.GraphQl.PhotovoltaicDataX;

public sealed class PhotovoltaicDataType
    : EntityType<PhotovoltaicData, PhotovoltaicDataByIdDataLoader>
{
    protected override void Configure(
        IObjectTypeDescriptor<PhotovoltaicData> descriptor
    )
    {
        base.Configure(descriptor);
        descriptor
            .Field(x => x.Locale)
            .Type<NonNullType<LocaleType>>();
        descriptor
            .Field(x => x.Resources)
            .ResolveWith<PhotovoltaicDataResolvers>(t => t.GetGetHttpsResources(default!, default!, default!));
        descriptor
            .Field("resourceTree")
            .ResolveWith<PhotovoltaicDataResolvers>(t => t.GetGetHttpsResourceTree(default!));
        descriptor
            .Field("timestamp")
            .Type<NonNullType<DateTimeType>>()
            .ResolveWith<PhotovoltaicDataResolvers>(t => t.GetTimestamp());
    }
}