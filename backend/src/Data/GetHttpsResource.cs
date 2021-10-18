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
        public Guid DataFormatId { get; private set; }
        public ICollection<FileMetaInformation> ArchivedFilesMetaInformation { get; private set; } = new List<FileMetaInformation>();

        public Guid DataId { get; private set; }

        // TODO [InverseProperty(nameof(IData.Resources))]
        [NotMapped]
        public IData? Data { get; set; }

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
          Guid dataFormatId,
          Guid dataId,
          Guid? parentId,
          ICollection<FileMetaInformation> archivedFilesMetaInformation,
          ToTreeVertexAppliedConversionMethod? appliedConversionMethod
        )
        : this(
            description: description,
            hashValue: hashValue,
            dataFormatId: dataFormatId,
            parentId: parentId,
            archivedFilesMetaInformation: archivedFilesMetaInformation,
            appliedConversionMethod: appliedConversionMethod
        )
        {
            DataId = dataId;
        }

        public GetHttpsResource(
          string description,
          string hashValue,
          Guid dataFormatId,
          Guid? parentId,
          ICollection<FileMetaInformation> archivedFilesMetaInformation,
          ToTreeVertexAppliedConversionMethod? appliedConversionMethod
        )
        : this(
            description: description,
            hashValue: hashValue,
            dataFormatId: dataFormatId,
            parentId: parentId
        )
        {
            ArchivedFilesMetaInformation = archivedFilesMetaInformation;
            AppliedConversionMethod = appliedConversionMethod;
        }

        // `DbContext` needs this constructor without owned entities.
        public GetHttpsResource(
          string description,
          string hashValue,
          Guid dataFormatId,
          Guid dataId,
          Guid? parentId
        )
        : this(
            description: description,
            hashValue: hashValue,
            dataFormatId: dataFormatId,
            parentId: parentId
        )
        {
            DataId = dataId;
        }

        public GetHttpsResource(
          string description,
          string hashValue,
          Guid dataFormatId,
          Guid? parentId
        )
        {
            Description = description;
            HashValue = hashValue;
            DataFormatId = dataFormatId;
            ParentId = parentId;
        }
    }
}
