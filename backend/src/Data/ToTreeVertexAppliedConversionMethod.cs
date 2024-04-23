using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database.Data;

[Owned]
public sealed class ToTreeVertexAppliedConversionMethod
{
    public ToTreeVertexAppliedConversionMethod(
        Guid methodId,
        ICollection<NamedMethodArgument> arguments,
        string sourceName
    )
        : this(
            methodId,
            sourceName
        )
    {
        Arguments = arguments;
    }

    // `DbContext` needs this constructor without owned entities.
    public ToTreeVertexAppliedConversionMethod(
        Guid methodId,
        string sourceName
    )
    {
        MethodId = methodId;
        SourceName = sourceName;
    }

    public Guid MethodId { get; private set; }
    public ICollection<NamedMethodArgument> Arguments { get; private set; } = new List<NamedMethodArgument>();
    public string SourceName { get; private set; }
}