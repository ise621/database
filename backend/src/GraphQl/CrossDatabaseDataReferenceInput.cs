using System;
using Database.Enumerations;

namespace Database.GraphQl
{
    public sealed record CrossDatabaseDataReferenceInput(
        Guid DataId,
        DateTime DataTimestamp,
        DataKind DataKind,
        Guid DatabaseId
    );
}