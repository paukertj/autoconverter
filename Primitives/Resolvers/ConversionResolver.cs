using System;

namespace Paukertj.Autoconverter.Primitives.Resolvers
{
    public class ConversionResolver<TFrom, TTo>
    {
        public ConversionResolver<TFrom, TTo> Resolve<TTarget, TDestination>(Func<TTo, TDestination> destionation, Func<TFrom, TTarget> target) 
        {
            return this;
        }

        public ConversionResolver<TFrom, TTo> Ignore<TDestination>(Func<TTo, TDestination> destionation)
        {
            return this;
        }
    }
}
