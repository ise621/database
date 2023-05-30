using System;
using System.Collections.Generic;

namespace Database.GraphQl
{
    public sealed record AppliedMethodInput(
          Guid MethodId,
          IReadOnlyList<NamedMethodArgumentInput> Arguments,
          IReadOnlyList<NamedMethodSourceInput> Sources
    );
}