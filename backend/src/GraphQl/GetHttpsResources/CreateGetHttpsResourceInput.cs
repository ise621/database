using System;
using System.Collections.Generic;
using Database.Enumerations;

namespace Database.GraphQl.GetHttpsResources;

public sealed record CreateGetHttpsResourceInput(
    string AccessToken,
    string Description,
    string HashValue,
    Guid DataFormatId,
    Guid DataId,
    DataKind DataKind,
    Guid? ParentId,
    IReadOnlyList<FileMetaInformationInput> ArchivedFilesMetaInformation,
    ToTreeVertexAppliedConversionMethodInput? AppliedConversionMethod
);