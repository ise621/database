using System;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    [Owned]
    public sealed class FileMetaInformation
    {
        public string[] Path { get; private set; }
        public Guid FormatId { get; private set; }

        public FileMetaInformation(
          string[] path,
          Guid formatId
        )
        {
            Path = path;
            FormatId = formatId;
        }
    }
}
