using CS.Toolkit.Mapper.Contracts.Attributes;
using System.Collections.Generic;

namespace CS.Mapper.Example.Datamodels.MegaCrapDataModels
{
    public class MegaCrapProduct
    {
        [Mapping(targetPropertyPaths: new[] { nameof(Common.CommonDataModel.Id), nameof(Common.CommonDataModel.Id.Gtin13) })]
        public string Ean { get; set; }
        [Mapping(targetPropertyPaths: new[] { nameof(Common.CommonDataModel.Id), nameof(Common.CommonDataModel.Id.ItemNumber) })]
        public string ManufacturerProductId { get; set; }
        [Mapping(targetPropertyPaths: new[] { nameof(Common.CommonDataModel.Description), nameof(Common.CommonDataModel.Description.Description) })]
        public string EnglishDescription { get; set; }
        public string KlingonDescription { get; set; }
        public List<MegaCrapFeature> Features { get; set; }
        [Mapping(targetPropertyPaths: new[] {nameof(Common.CommonDataModel.Logistics)})]
        public MegaCrapLogistic Logistic1 { get; set; }
        [Mapping(targetPropertyPaths: new[] { nameof(Common.CommonDataModel.Logistics) })]
        public MegaCrapLogistic Logistic2 { get; set; }
        [Mapping(targetPropertyPaths: new[] { nameof(Common.CommonDataModel.Logistics) })]
        public MegaCrapLogistic Logistic3 { get; set; }
        [Mapping(targetPropertyPaths: new[] { nameof(Common.CommonDataModel.Logistics) })]
        public MegaCrapLogistic Logistic4 { get; set; }
        [Mapping]
        public string Manufacturer { get; set; }
        [Mapping(targetPropertyPath: nameof(Common.CommonDataModel.ProductState))]
        public string State { get; set; }
    }
}
