using HotChocolate.Data.Filters;

namespace Database.GraphQl.OpticalDataX
{
    public sealed class OpticalDataFilterType
      : FilterInputType<Data.OpticalData>
    {
        protected override void Configure(
          IFilterInputTypeDescriptor<Data.OpticalData> descriptor
          )
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.Name);
            descriptor.Field(x => x.Description);
            descriptor.Field(x => x.ComponentId);
            descriptor.Field(x => x.CreatorId);
            descriptor.Field(x => x.CreatedAt);
            // descriptor.Field(x => x.NearnormalHemisphericalSolarReflectances);
            // descriptor.Field(x => x.NearnormalHemisphericalSolarTransmittances);
            // descriptor.Field(x => x.NearnormalHemisphericalVisibleReflectances);
            // descriptor.Field(x => x.NearnormalHemisphericalVisibleTransmittances);
            // descriptor.Field(x => x.InfraredEmittances);
            // descriptor.Field(x => x.ColorRenderingIndices);
            // descriptor.Field(x => x.CielabColors);
        }
    }
}
