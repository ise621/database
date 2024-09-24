using System.Collections.Generic;

namespace Database.GraphQl.GeometricDataX;

public sealed class CreateGeometricDataError
    : UserErrorBase<CreateGeometricDataErrorCode>
{
    public CreateGeometricDataError(
        CreateGeometricDataErrorCode code,
        string message,
        IReadOnlyList<string> path
    )
        : base(code, message, path)
    {
    }
}