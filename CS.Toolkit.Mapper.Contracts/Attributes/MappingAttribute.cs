using CS.Toolkit.Mapper.Contracts.Converters;
using System;

namespace CS.Toolkit.Mapper.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MappingAttribute: Attribute
    {
        public string TargetPropertyPath { get; }
        public bool IsComplex { get; }
        public string[] TargetPropertySubPaths { get; }
        public IConverter ValueConverter { get; }
        public bool HasConverter => ValueConverter != null;

        public MappingAttribute()
        {
        }

        public MappingAttribute(string targetPropertyPath) : this()
        {            
            TargetPropertyPath = targetPropertyPath;
        }

        public MappingAttribute(string targetPropertyPath, bool isComplex = false) : this(targetPropertyPath)
        {
            IsComplex = isComplex;
        }

        public MappingAttribute(string[] targetPropertyPaths) : this(targetPropertyPaths[0], true)
        {
            TargetPropertySubPaths = targetPropertyPaths;
        }

        public MappingAttribute(string targetPropertyPath, Type converterType, bool isComplex = false) : this(targetPropertyPath)
        {
            ValueConverter = converterType.GetConstructor(Type.EmptyTypes).Invoke(Array.Empty<object>()) as IConverter;
            IsComplex = isComplex;
        }
    }
}
