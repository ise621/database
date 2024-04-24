using System.Diagnostics.CodeAnalysis;

namespace Database.GraphQl.Databases;

[SuppressMessage("Naming", "CA1707")]
public enum UpdateDatabaseErrorCode
{
    UNKNOWN,
    UNAUTHORIZED,
    UNKNOWN_DATABASE
}