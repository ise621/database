using System;
using System.Collections.Generic;

namespace Database.GraphQl
{
    public record RootGetHttpsResourceInput(
          string Description,
          string HashValue,
          Guid DataFormatId,
          IReadOnlyList<FileMetaInformationInput> ArchivedFilesMetaInformation,
          ToTreeVertexAppliedConversionMethodInput? AppliedConversionMethod
        );
}
