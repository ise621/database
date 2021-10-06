using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Data
{
    public sealed class GetHttpsResource
    : Data.Entity
    {
        public string Description { get; private set; }
        public string HashValue { get; private set; }
        public Uri Locator { get; private set; }
        public Guid FormatId { get; private set; }
        public ICollection<FileMetaInformation> ArchivedFilesMetaInformation { get; private set; }

        public Guid DataId { get; private set; }

        [InverseProperty(nameof(DataX.Resources))]
        public DataX? Data { get; set; }

        public Guid? ParentId { get; private set; }
        // TODO Require the conversion method to be given whenever there is a parent. In other words, either both are `null` or both are non-`null`.
        public ToTreeVertexAppliedConversionMethod? AppliedConversionMethod { get; private set; }

        // TODO The parent's `Data` must be the same as this resource's `Data`.
        [InverseProperty(nameof(Children))]
        public GetHttpsResource? Parent { get; set; }

        [InverseProperty(nameof(Parent))]
        public ICollection<GetHttpsResource> Children { get; } = new List<GetHttpsResource>();

        public GetHttpsResource(
          string description,
          string hashValue,
          Uri locator,
          Guid formatId,
          ICollection<FileMetaInformation> archivedFilesMetaInformation,
          Guid dataId,
          Guid? parentId,
          ToTreeVertexAppliedConversionMethod? appliedConversionMethod
        )
        {
            Description = description;
            HashValue = hashValue;
            Locator = locator;
            FormatId = formatId;
            ArchivedFilesMetaInformation = archivedFilesMetaInformation;
            DataId = dataId;
            ParentId = parentId;
            AppliedConversionMethod = appliedConversionMethod;
        }
    }
}
