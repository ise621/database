using System;
using System.Collections.Generic;

namespace Database.GraphQl.GetHttpsResources
{
    public record CreateGetHttpsResourceInput(
          string AccessToken,
          string Description,
          string HashValue,
          Guid DataFormatId,
          Guid DataId,
          Guid? ParentId,
          IReadOnlyList<FileMetaInformationInput> ArchivedFilesMetaInformation,
          ToTreeVertexAppliedConversionMethodInput? AppliedConversionMethod
        );
}
