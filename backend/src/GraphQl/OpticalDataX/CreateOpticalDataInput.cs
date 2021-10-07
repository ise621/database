using System;
using System.Collections.Generic;

namespace Database.GraphQl.OpticalDataX
{
    public record CreateOpticalDataInput(
          string Locale,
          Guid ComponentId,
          string? Name,
          string? Description,
          string[] Warnings,
          DateTime CreatedAt,
          Guid CreatorId,
          AppliedMethodInput AppliedMethod,
          IReadOnlyList<DataApprovalInput> Approvals,
          // ResponseApproval Approval
          double[] NearnormalHemisphericalVisibleTransmittances,
          double[] NearnormalHemisphericalVisibleReflectances,
          double[] NearnormalHemisphericalSolarTransmittances,
          double[] NearnormalHemisphericalSolarReflectances,
          double[] InfraredEmittances,
          double[] ColorRenderingIndices,
          IReadOnlyList<CielabColorInput> CielabColors
        );
}
