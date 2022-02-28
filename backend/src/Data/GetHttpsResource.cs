using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Extensions;

namespace Database.Data
{
    public sealed class GetHttpsResource
    : Data.Entity
    {
        public static string ConstructVertexId(Guid id)
        {
            return id.ToString("D").Base64Encode();
        }

        public string Description { get; private set; }
        public string HashValue { get; private set; }
        public Guid DataFormatId { get; private set; }
        public ICollection<FileMetaInformation> ArchivedFilesMetaInformation { get; private set; } = new List<FileMetaInformation>();

        // TODO Make sure that at least one ID is always present. In that case `Guid.Empty` should never be used!
        [NotMapped]
        public Guid DataId { get => CalorimetricDataId ?? HygrothermalDataId ?? OpticalDataId ?? PhotovoltaicDataId ?? Guid.Empty; }
        [NotMapped]
        public IData? Data { get => CalorimetricData ?? HygrothermalData ?? OpticalData ?? PhotovoltaicData as IData; }

        public Guid? CalorimetricDataId { get; private set; }
        [InverseProperty(nameof(Database.Data.CalorimetricData.Resources))]
        public CalorimetricData? CalorimetricData { get; set; }

        public Guid? HygrothermalDataId { get; private set; }

        [InverseProperty(nameof(Database.Data.HygrothermalData.Resources))]
        public HygrothermalData? HygrothermalData { get; set; }

        public Guid? OpticalDataId { get; private set; }

        [InverseProperty(nameof(Database.Data.OpticalData.Resources))]
        public OpticalData? OpticalData { get; set; }

        public Guid? PhotovoltaicDataId { get; private set; }

        [InverseProperty(nameof(Database.Data.PhotovoltaicData.Resources))]
        public PhotovoltaicData? PhotovoltaicData { get; set; }


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
          Guid? calorimetricDataId,
          Guid? hygrothermalDataId,
          Guid? opticalDataId,
          Guid? photovoltaicDataId,
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
            CalorimetricDataId = calorimetricDataId;
            HygrothermalDataId = hygrothermalDataId;
            OpticalDataId = opticalDataId;
            PhotovoltaicDataId = photovoltaicDataId;
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
          Guid? calorimetricDataId,
          Guid? hygrothermalDataId,
          Guid? opticalDataId,
          Guid? photovoltaicDataId,
          Guid? parentId
        )
        : this(
            description: description,
            hashValue: hashValue,
            dataFormatId: dataFormatId,
            parentId: parentId
        )
        {
            CalorimetricDataId = calorimetricDataId;
            HygrothermalDataId = hygrothermalDataId;
            OpticalDataId = opticalDataId;
            PhotovoltaicDataId = photovoltaicDataId;
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
