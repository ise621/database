using System;
using System.Collections.Generic;

namespace Database.GraphQl
{
    public record AppliedMethodInput(
          Guid MethodId,
          IReadOnlyList<NamedMethodArgumentInput> Arguments,
          IReadOnlyList<NamedMethodSourceInput> Sources
    );
}