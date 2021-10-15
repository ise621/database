using System;
using System.Collections.Generic;

namespace Database.Data
{
    public sealed class CalorimetricData
    : DataX
    {
        public double[] GValues { get; private set; }
        public double[] UValues { get; private set; }

        public CalorimetricData(
          string locale,
          Guid componentId,
          string? name,
          string? description,
          string[] warnings,
          Guid creatorId,
          DateTime createdAt,
          AppliedMethod appliedMethod,
          ICollection<DataApproval> approvals,
          // ResponseApproval approval
          double[] gValues,
          double[] uValues
        ) : base(
          locale: locale,
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
            GValues = gValues;
            UValues = uValues;
        }

        // `DbContext` needs this constructor without owned entities.
        public CalorimetricData(
          string locale,
          Guid componentId,
          string? name,
          string? description,
          string[] warnings,
          Guid creatorId,
          DateTime createdAt,
          // ResponseApproval approval
          double[] gValues,
          double[] uValues
        ) : base(
          locale: locale,
          componentId: componentId,
          name: name,
          description: description,
          warnings: warnings,
          creatorId: creatorId,
          createdAt: createdAt
        )
        {
            GValues = gValues;
            UValues = uValues;
        }
    }
}
