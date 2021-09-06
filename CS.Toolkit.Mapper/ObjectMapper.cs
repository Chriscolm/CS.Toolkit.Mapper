using CS.Toolkit.Mapper.Contracts;
using CS.Toolkit.Mapper.Contracts.Attributes;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace CS.Toolkit.Mapper
{
    public class ObjectMapper : IObjectMapper
    {
        public TOut Map<TIn, TOut>(TIn source) where TIn : new() where TOut : new()
        {
            var result = Reflect<TIn, TOut>(source);
            return result;
        }

        protected TOut Reflect<TIn, TOut>(TIn source) where TOut : new()
        {
            if(source == null)
            {
                return default;
            }
            var res = new TOut();

            var sourceProps = source.GetType().GetProperties().Where(p => p.GetCustomAttribute<MappingAttribute>() != null);
            var targetProps = res.GetType().GetProperties();

            foreach(var sourceProp in sourceProps)
            {
                var attribute = sourceProp.GetCustomAttribute<MappingAttribute>(inherit: true);
                var targetPath = attribute.TargetPropertyPath ?? sourceProp.Name;
                var targetProp = targetProps.SingleOrDefault(p => p.Name == targetPath);
                if (attribute.IsComplex)
                {
                    SetComplexValue(sourceProp, targetProp, source, res, attribute);
                }
                else
                {
                    SetValue(sourceProp, targetProp, source, res, attribute);
                }                
            }

            return res;
        }

        private void SetValue(PropertyInfo sourceProp, PropertyInfo targetProp, object source, object target, MappingAttribute attribute)
        {
            var v = sourceProp.GetValue(source);
            if (attribute.HasConverter)
            {
                var converted = attribute.ValueConverter.Convert(v);
                targetProp.SetValue(target, converted);
            }
            else
            {
                targetProp.SetValue(target, v);
            }
        }

        private void SetComplexValue(PropertyInfo sourceProp, PropertyInfo targetProp, object source, object target, MappingAttribute attribute)
        {            
            var isManyToOne = attribute.TargetPropertySubPaths?.Any() ?? false;
            if (isManyToOne)
            {
                // Quelltyp ist nicht komplex -> mehrere Quelleigenschaften auf zusammengesetzte Zieleigenschaft verteilen
                SetComplexManyToOneValue(sourceProp, targetProp, source, target, attribute);
            }
            else
            {
                SetComplexValue(sourceProp, targetProp, source, target);
            }
        }

        private void SetComplexManyToOneValue(PropertyInfo sourceProp, PropertyInfo targetProp, object source, object target, MappingAttribute attribute)
        {
            // komplexen Zieltyp erzeugen, falls er noch nicht existiert            
            var complex = targetProp.GetValue(target);
            if(complex == null)
            {
                complex = targetProp.PropertyType.GetConstructor(Type.EmptyTypes).Invoke(null);
                targetProp.SetValue(target, complex);
            }
            if (complex is IList enumerable)
            {                
                var t = targetProp.PropertyType.GetGenericArguments();                
                var mapper = new ObjectMapper();
                var method = mapper.GetType().GetMethod(nameof(ObjectMapper.Map));
                var genericMethod = method.MakeGenericMethod(new[] { sourceProp.PropertyType, t[0] });
                var value = sourceProp.GetValue(source);
                var res = genericMethod.Invoke(mapper, new[] { value });
                if (res != null)
                {
                    var add = enumerable.GetType().GetMethod(nameof(IList.Add));
                    add.Invoke(complex, new[] { res });
                }
            }
            else
            {
                // zu Demonstrationszwecken seien die Property-Pfade niemals mehr als zwei Elemente lang
                var name = attribute.TargetPropertySubPaths.Last();
                var t = complex.GetType().GetProperty(name);
                SetValue(sourceProp, t, source, complex, attribute);
            }
        }

        private void SetComplexValue(PropertyInfo sourceProp, PropertyInfo targetProp, object source, object target)
        {
            // Zieltyp ist komplex, neue Instanz erzeugen
            var t = targetProp.PropertyType.GetConstructor(Type.EmptyTypes).Invoke(null);
            // Map-Methode aus dem ObjectMapper holen und mit neuen Typparametern versorgen 
            var v = sourceProp.GetValue(source);
            var mapper = new ObjectMapper();
            var m = mapper.GetType().GetMethod(nameof(mapper.Map))
                .MakeGenericMethod(v.GetType(), targetProp.PropertyType);
            var res = m.Invoke(mapper, new[] { v });
            targetProp.SetValue(target, res);
        }
    }
}
