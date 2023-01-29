using System;
using System.Linq.Expressions;

namespace Paukertj.Autoconverter.Primitives.Resolvers
{
    public class ConversionResolver<TFrom, TTo>
    {
        public ConversionResolver<TFrom, TTo> Resolve<TTarget, TDestination>(Expression<Func<TFrom, TTarget>> target, Expression<Func<TTo, TDestination>> destionation) 
        {
            return this;
        }

        public ConversionResolver<TFrom, TTo> Ignore<TDestination>(Expression<Func<TTo, TDestination>> destionation)
        {
            return this;
        }
    }
}
