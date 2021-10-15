using System;
using System.Collections.Generic;

namespace Database.Data
{
    public sealed class HygrothermalData
    : DataX
    {
        public HygrothermalData(
          string locale,
          Guid componentId,
          string? name,
          string? description,
          string[] warnings,
          Guid creatorId,
          DateTime createdAt,
          AppliedMethod appliedMethod,
          ICollection<DataApproval> approvals
        // ResponseApproval approval
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
        }

        // `DbContext` needs this constructor without owned entities.
        public HygrothermalData(
          string locale,
          Guid componentId,
          string? name,
          string? description,
          string[] warnings,
          Guid creatorId,
          DateTime createdAt
        // ResponseApproval approval
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
        }
    }
}
