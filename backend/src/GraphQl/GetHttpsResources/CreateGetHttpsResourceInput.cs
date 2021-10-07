using System;
using System.Collections.Generic;

namespace Database.GraphQl.GetHttpsResources
{
    public record CreateGetHttpsResourceInput(
          string Description,
          string HashValue,
          Uri Locator,
          Guid FormatId,
          Guid DataId,
          Guid? ParentId,
          IReadOnlyList<FileMetaInformationInput> ArchivedFilesMetaInformation,
          ToTreeVertexAppliedConversionMethodInput? AppliedConversionMethod
        );
}
