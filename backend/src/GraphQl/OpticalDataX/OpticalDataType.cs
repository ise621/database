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
                .Field(x => x.Resources)
                .ResolveWith<OpticalDataResolvers>(t => t.GetGetHttpsResources(default!, default!, default!));
        }
    }
}
