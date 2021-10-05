using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Data
{
    public abstract class DataX
    : Data.Entity, IData
    {
        public DateTime Timestamp { get; private set; }
        public string Locale { get; private set; }
        public Guid DatabaseId { get; private set; }
        public Guid ComponentId { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public string[] Warnings { get; private set; }
        public Guid CreatorId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public AppliedMethod AppliedMethod { get; private set; }
        public ICollection<DataApproval> Approvals { get; private set; }
        // public ResponseApproval Approval { get; private set; }

        public Guid? ResourceId { get; set; }

        [InverseProperty(nameof(GetHttpsResource.Data))]
        public GetHttpsResource? Resource { get; set; }

        protected DataX(
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
          ICollection<DataApproval> approvals
        // ResponseApproval approval
        )
        {
            Timestamp = timestamp;
            Locale = locale;
            DatabaseId = databaseId;
            ComponentId = componentId;
            Name = name;
            Description = description;
            Warnings = warnings;
            CreatorId = creatorId;
            CreatedAt = createdAt;
            AppliedMethod = appliedMethod;
            Approvals = approvals;
            // Approval = approval;
        }
    }
}
