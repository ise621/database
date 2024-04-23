using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database.Data;

[Owned]
public sealed class AppliedMethod
{
    public AppliedMethod(
        Guid methodId,
        ICollection<NamedMethodArgument> arguments,
        ICollection<NamedMethodSource> sources
    )
        : this(methodId)
    {
        Arguments = arguments;
        Sources = sources;
    }

    // `DbContext` needs this constructor without owned entities.
    public AppliedMethod(
        Guid methodId
    )
    {
        MethodId = methodId;
    }

    public Guid MethodId { get; private set; }
    public ICollection<NamedMethodArgument> Arguments { get; private set; } = new List<NamedMethodArgument>();
    public ICollection<NamedMethodSource> Sources { get; private set; } = new List<NamedMethodSource>();
}