using System;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    [Owned]
    public sealed class FileMetaInformation
    {
        public string[] Path { get; private set; }
        public Guid DataFormatId { get; private set; }
        // TODO Remove the following
        public Guid FormatId { get => DataFormatId; }

        public FileMetaInformation(
          string[] path,
          Guid dataFormatId
        )
        {
            Path = path;
            DataFormatId = dataFormatId;
        }
    }
}
