using System;

namespace Paukertj.Autoconverter.Primitives.Resolvers
{
    public abstract class Resolver
    {
        public abstract void Resolve();

        public ConversionResolver<TFrom, TTo> Map<TFrom, TTo>()
        {
            return new ConversionResolver<TFrom, TTo>();
        }

        public string ReturnTest()
        {
            return "Ok!";
        }
    }
}
