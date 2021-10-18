using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Data
{
    public sealed class HygrothermalData
    : DataX
    {
        [InverseProperty(nameof(GetHttpsResource.HygrothermalData))]
        public override ICollection<GetHttpsResource> Resources { get; } = new List<GetHttpsResource>();

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
