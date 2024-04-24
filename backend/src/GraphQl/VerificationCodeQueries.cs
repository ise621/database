using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl;

[ExtendObjectType(nameof(Query))]
public sealed class VerificationCodeQueries
{
    public string GetVerificationCode(
        [Service] AppSettings appSettings
    )
    {
        return appSettings.VerificationCode;
    }
}