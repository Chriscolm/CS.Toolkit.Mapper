using CS.Toolkit.Mapper.Contracts.Attributes;
using CS.Toolkit.Mapper.Contracts.Converters;

namespace CS.Mapper.Example.Datamodels.MegaCrapDataModels
{
    public class MegaCrapLogistic
    {
        [Mapping(targetPropertyPath: nameof(Common.ProductLogistic.Width), converterType:typeof(CompoundStringToDoubleConverter))]
        public string Width { get; set; }
        [Mapping(targetPropertyPath: nameof(Common.ProductLogistic.Height), converterType: typeof(CompoundStringToDoubleConverter))]
        public string Height { get; set; }
        [Mapping(targetPropertyPath: nameof(Common.ProductLogistic.Length), converterType: typeof(CompoundStringToDoubleConverter))]
        public string Length { get; set; }
        [Mapping(targetPropertyPath: nameof(Common.ProductLogistic.PackingUnit))]
        public string PackingUnit { get; set; }
    }
}
