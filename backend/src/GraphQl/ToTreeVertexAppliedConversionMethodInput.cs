using System;
using System.Collections.Generic;

namespace Database.GraphQl
{
    public record ToTreeVertexAppliedConversionMethodInput(
            Guid MethodId,
            IReadOnlyList<NamedMethodArgumentInput> Arguments,
            string SourceName
    );
}