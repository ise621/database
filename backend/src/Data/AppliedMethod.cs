using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    [Owned]
    public sealed class AppliedMethod
    {
        public Guid MethodId { get; private set; }
        public ICollection<NamedMethodArgument> Arguments { get; private set; }
        public ICollection<NamedMethodSource> Sources { get; private set; }

        public AppliedMethod(
          Guid methodId,
          ICollection<NamedMethodArgument> arguments,
          ICollection<NamedMethodSource> sources
        )
        {
            MethodId = methodId;
            Arguments = arguments;
            Sources = sources;
        }
    }
}