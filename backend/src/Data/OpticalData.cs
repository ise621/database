using System;
using System.Collections.Generic;

namespace Database.Data
{
    public sealed class OpticalData
    : DataX
    {
        public OpticalData(
          DateTime timestamp,
          string locale,
          Guid databaseId,
          Guid componentId,
          string? name,
          string? description,
          string[] warnings,
          Guid creatorId,
          DateTime createdAt,
          AppliedMethod appliedMethod,
          ICollection<DataApproval> approvals,
          // ResponseApproval approval
          double[] nearnormalHemisphericalVisibleTransmittances,
          double[] nearnormalHemisphericalVisibleReflectances,
          double[] nearnormalHemisphericalSolarTransmittances,
          double[] nearnormalHemisphericalSolarReflectances,
          double[] infraredEmittances,
          double[] colorRenderingIndices,
          ICollection<CielabColor> cielabColors
        ) : base(
          timestamp: timestamp,
          locale: locale,
          databaseId: databaseId,
          componentId: componentId,
          name: name,
          description: description,
          warnings: warnings,
          creatorId: creatorId,
          createdAt: createdAt,
          appliedMethod: appliedMethod,
          approvals: approvals
        )
        {
            NearnormalHemisphericalVisibleTransmittances = nearnormalHemisphericalVisibleTransmittances;
            NearnormalHemisphericalVisibleReflectances = nearnormalHemisphericalVisibleReflectances;
            NearnormalHemisphericalSolarTransmittances = nearnormalHemisphericalSolarTransmittances;
            NearnormalHemisphericalSolarReflectances = nearnormalHemisphericalSolarReflectances;
            InfraredEmittances = infraredEmittances;
            ColorRenderingIndices = colorRenderingIndices;
            CielabColors = cielabColors;
        }

        public double[] NearnormalHemisphericalVisibleTransmittances { get; private set; }
        public double[] NearnormalHemisphericalVisibleReflectances { get; private set; }
        public double[] NearnormalHemisphericalSolarTransmittances { get; private set; }
        public double[] NearnormalHemisphericalSolarReflectances { get; private set; }
        public double[] InfraredEmittances { get; private set; }
        public double[] ColorRenderingIndices { get; private set; }
        public ICollection<CielabColor> CielabColors { get; private set; }
    }
}
