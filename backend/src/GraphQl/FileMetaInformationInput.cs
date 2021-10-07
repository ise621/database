using System;

namespace Database.GraphQl
{
    public record FileMetaInformationInput(
          string[] Path,
          Guid FormatId
    );
}