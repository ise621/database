using Database.Data;
using HotChocolate.Types;

namespace Database.GraphQl.GeometricDataX;

public sealed class GeometricDataType
    : EntityType<GeometricData, GeometricDataByIdDataLoader>
{
    protected override void Configure(
        IObjectTypeDescriptor<GeometricData> descriptor
    )
    {
        base.Configure(descriptor);
        descriptor
            .Field(x => x.Locale)
            .Type<NonNullType<LocaleType>>();
        descriptor
            .Field(x => x.Resources)
            .ResolveWith<GeometricDataResolvers>(t => t.GetGetHttpsResources(default!, default!, default!));
        descriptor
            .Field("resourceTree")
            .ResolveWith<GeometricDataResolvers>(t => t.GetGetHttpsResourceTree(default!));
        descriptor
            .Field("timestamp")
            .Type<NonNullType<DateTimeType>>()
            .ResolveWith<GeometricDataResolvers>(t => t.GetTimestamp());
    }
}