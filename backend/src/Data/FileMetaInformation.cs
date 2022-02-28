using System;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    [Owned]
    public sealed class FileMetaInformation
    {
        public string[] Path { get; private set; }
        public Guid DataFormatId { get; private set; }

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
