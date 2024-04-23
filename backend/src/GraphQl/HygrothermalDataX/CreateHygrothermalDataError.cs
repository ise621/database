using System.Collections.Generic;

namespace Database.GraphQl.HygrothermalDataX;

public sealed class CreateHygrothermalDataError
    : UserErrorBase<CreateHygrothermalDataErrorCode>
{
    public CreateHygrothermalDataError(
        CreateHygrothermalDataErrorCode code,
        string message,
        IReadOnlyList<string> path
    )
        : base(code, message, path)
    {
    }
}