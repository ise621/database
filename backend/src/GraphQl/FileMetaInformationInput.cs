using System;

namespace Database.GraphQl
{
    public sealed record FileMetaInformationInput(
          string[] Path,
          Guid DataFormatId
    );
}