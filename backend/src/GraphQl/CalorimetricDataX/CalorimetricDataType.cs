using HotChocolate.Types;

namespace Database.GraphQl.CalorimetricDataX
{
    public sealed class CalorimetricDataType
      : EntityType<Data.CalorimetricData, CalorimetricDataByIdDataLoader>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Data.CalorimetricData> descriptor
            )
        {
            base.Configure(descriptor);
            descriptor
                .Field(x => x.Locale)
                .Type<NonNullType<LocaleType>>();
            descriptor
                .Field(x => x.Resources)
                .ResolveWith<CalorimetricDataResolvers>(t => t.GetGetHttpsResources(default!, default!, default!));
            descriptor
                .Field("resourceTree")
                .ResolveWith<CalorimetricDataResolvers>(t => t.GetGetHttpsResourceTree(default!));
            descriptor
                .Field("timestamp")
                .Type<NonNullType<DateTimeType>>()
                .ResolveWith<CalorimetricDataResolvers>(t => t.GetTimestamp());
        }
    }
}
