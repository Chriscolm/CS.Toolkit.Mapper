using System;

namespace CS.Toolkit.Mapper.Contracts
{
    public interface IObjectMapper
    {
        TOut Map<TIn, TOut>(TIn source) where TIn : new() where TOut : new();
    }
}
