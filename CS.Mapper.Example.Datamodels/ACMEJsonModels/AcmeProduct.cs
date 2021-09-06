using CS.Toolkit.Mapper.Contracts.Attributes;
using System.Collections.Generic;

namespace CS.Mapper.Example.Datamodels.ACMEJsonModels
{
    public class AcmeProduct
    {
        [Mapping(targetPropertyPath: nameof(Common.CommonDataModel.Id), isComplex: true)]
        public AcmeIdentifier ProductKey { get; set; }
        [Mapping(targetPropertyPath: nameof(Common.CommonDataModel.Description), isComplex: true)]
        public AcmeDescription Description { get; set; }
        public List<AcmeFeature> Features { get; set; }
        public List<AcmeLogistic> Logistics { get; set; }
        [Mapping]
        public string Manufacturer { get; set; }
        [Mapping(targetPropertyPath: nameof(Common.CommonDataModel.ProductState))]
        public string Status { get; set; }

    }
}
