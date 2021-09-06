using CS.Toolkit.Mapper.Contracts.Attributes;

namespace CS.Mapper.Example.Datamodels.ACMEJsonModels
{
    public class AcmeIdentifier
    {
        [Mapping]
        public string Gtin13 { get; set; }
        [Mapping]
        public string ItemNumber { get; set; }        
    }
}
