using System;
using System.Collections.Generic;

namespace Database.Data;

public abstract class DataX
    : Entity, IData
{
    protected DataX(
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
    )
        : this(
            locale,
            componentId,
            name,
            description,
            warnings,
            creatorId,
            createdAt
        )
    {
        AppliedMethod = appliedMethod;
        Approvals = approvals;
        // Approval = approval;
    }

    // `DbContext` needs this constructor without owned entities.
    protected DataX(
        string locale,
        Guid componentId,
        string? name,
        string? description,
        string[] warnings,
        Guid creatorId,
        DateTime createdAt
    )
    {
        Locale = locale;
        ComponentId = componentId;
        Name = name;
        Description = description;
        Warnings = warnings;
        CreatorId = creatorId;
        CreatedAt = createdAt;
    }

    public string Locale { get; }
    public Guid ComponentId { get; }
    public string? Name { get; }
    public string? Description { get; }
    public string[] Warnings { get; }
    public Guid CreatorId { get; }
    public DateTime CreatedAt { get; }
    public AppliedMethod AppliedMethod { get; } = default!;

    public ICollection<DataApproval> Approvals { get; } = new List<DataApproval>();
    // public ResponseApproval Approval { get; private set; }

    // TODO Exactly one resource must not have a parent and each other resource must have one from this list and the graph must be connected. In other words, the resources must form a tree.
    public virtual ICollection<GetHttpsResource> Resources { get; } = new List<GetHttpsResource>();
}