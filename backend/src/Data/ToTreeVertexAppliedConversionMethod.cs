using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    [Owned]
    public sealed class ToTreeVertexAppliedConversionMethod
    {
        public Guid MethodId { get; private set; }
        public ICollection<NamedMethodArgument> Arguments { get; private set; }
        public string SourceName { get; private set; }

        public ToTreeVertexAppliedConversionMethod(
            Guid methodId,
            ICollection<NamedMethodArgument> arguments,
            string sourceName
        )
        {
            MethodId = methodId;
            Arguments = arguments;
            SourceName = sourceName;
        }
    }
}
