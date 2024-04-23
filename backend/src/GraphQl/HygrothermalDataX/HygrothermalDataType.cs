using Database.Data;
using HotChocolate.Types;

namespace Database.GraphQl.HygrothermalDataX;

public sealed class HygrothermalDataType
    : EntityType<HygrothermalData, HygrothermalDataByIdDataLoader>
{
    protected override void Configure(
        IObjectTypeDescriptor<HygrothermalData> descriptor
    )
    {
        base.Configure(descriptor);
        descriptor
            .Field(x => x.Locale)
            .Type<NonNullType<LocaleType>>();
        descriptor
            .Field(x => x.Resources)
            .ResolveWith<HygrothermalDataResolvers>(t => t.GetGetHttpsResources(default!, default!, default!));
        descriptor
            .Field("resourceTree")
            .ResolveWith<HygrothermalDataResolvers>(t => t.GetGetHttpsResourceTree(default!));
        descriptor
            .Field("timestamp")
            .Type<NonNullType<DateTimeType>>()
            .ResolveWith<HygrothermalDataResolvers>(t => t.GetTimestamp());
    }
}