using System;
using Microsoft.EntityFrameworkCore;
using Database.Enumerations;

namespace Database.Data
{
    [Owned]
    public sealed class CrossDatabaseDataReference
    {
        public Guid DataId { get; private set; }
        public DateTime DataTimestamp { get; private set; }
        public DataKind DataKind { get; private set; }
        public Guid DatabaseId { get; private set; }

        public CrossDatabaseDataReference(
        Guid dataId,
        DateTime dataTimestamp,
        DataKind dataKind,
        Guid databaseId
        )
        {
            DataId = dataId;
            DataTimestamp = dataTimestamp;
            DataKind = dataKind;
            DatabaseId = databaseId;
        }
    }
}
