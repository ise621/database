using System.Collections.Generic;
using Enum = System.Enum;

namespace Database.GraphQl;

public abstract class UserErrorBase<TUserErrorCode>
    : IUserError
    where TUserErrorCode : struct, Enum
{
    protected UserErrorBase(
        TUserErrorCode code,
        string message,
        IReadOnlyList<string> path
    )
    {
        Code = code;
        Message = message;
        Path = path;
    }

    public TUserErrorCode Code { get; }
    public string Message { get; }
    public IReadOnlyList<string> Path { get; }
}