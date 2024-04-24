using System.Collections.Generic;

namespace Database.GraphQl.CalorimetricDataX;

public sealed class CreateCalorimetricDataError
    : UserErrorBase<CreateCalorimetricDataErrorCode>
{
    public CreateCalorimetricDataError(
        CreateCalorimetricDataErrorCode code,
        string message,
        IReadOnlyList<string> path
    )
        : base(code, message, path)
    {
    }
}