using System.Collections.Generic;

namespace Database.GraphQl.OpticalDataX;

public sealed class CreateOpticalDataError
    : UserErrorBase<CreateOpticalDataErrorCode>
{
    public CreateOpticalDataError(
        CreateOpticalDataErrorCode code,
        string message,
        IReadOnlyList<string> path
    )
        : base(code, message, path)
    {
    }
}