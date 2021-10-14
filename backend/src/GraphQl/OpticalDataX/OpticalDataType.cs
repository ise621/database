using HotChocolate.Types;

namespace Database.GraphQl.OpticalDataX
{
    public sealed class OpticalDataType
      : EntityType<Data.OpticalData, OpticalDataByIdDataLoader>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Data.OpticalData> descriptor
            )
        {
            base.Configure(descriptor);
            descriptor
                .Field(x => x.Locale)
                .Type<NonNullType<LocaleType>>();
            descriptor
                .Field(x => x.Resources)
                .ResolveWith<OpticalDataResolvers>(t => t.GetGetHttpsResources(default!, default!, default!));
            descriptor
                .Field("resourceTree")
                .ResolveWith<OpticalDataResolvers>(t => t.GetGetHttpsResourceTree(default!));
            descriptor
                .Field("timestamp")
                .Type<NonNullType<DateTimeType>>()
                .ResolveWith<OpticalDataResolvers>(t => t.GetTimestamp());
        }
    }
}
