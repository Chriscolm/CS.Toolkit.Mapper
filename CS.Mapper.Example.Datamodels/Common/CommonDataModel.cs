using System.Collections.Generic;

namespace CS.Mapper.Example.Datamodels.Common
{
    public class CommonDataModel
    {
        public ProductIdentifier Id { get; set; }
        public ProductDescription Description { get; set; }
        public List<ProductFeature> Features { get; set; }
        public List<ProductLogistic> Logistics { get; set; }
        public string Manufacturer { get; set; }
        public string ProductState { get; set; }
    }
}
