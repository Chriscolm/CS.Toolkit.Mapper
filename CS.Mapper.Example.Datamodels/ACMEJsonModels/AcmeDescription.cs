using CS.Toolkit.Mapper.Contracts.Attributes;

namespace CS.Mapper.Example.Datamodels.ACMEJsonModels
{
    public class AcmeDescription
    {
        [Mapping]
        public string Description { get; set; }
    }
}
