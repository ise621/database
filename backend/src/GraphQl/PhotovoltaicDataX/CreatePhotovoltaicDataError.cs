using System.Collections.Generic;

namespace Database.GraphQl.PhotovoltaicDataX;

public sealed class CreatePhotovoltaicDataError
    : UserErrorBase<CreatePhotovoltaicDataErrorCode>
{
    public CreatePhotovoltaicDataError(
        CreatePhotovoltaicDataErrorCode code,
        string message,
        IReadOnlyList<string> path
    )
        : base(code, message, path)
    {
    }
}