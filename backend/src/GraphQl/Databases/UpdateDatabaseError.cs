using System.Collections.Generic;

namespace Database.GraphQl.Databases;

public sealed class UpdateDatabaseError
    : UserErrorBase<UpdateDatabaseErrorCode>
{
    public UpdateDatabaseError(
        UpdateDatabaseErrorCode code,
        string message,
        IReadOnlyList<string> path
    )
        : base(code, message, path)
    {
    }
}