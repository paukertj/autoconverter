using Paukertj.Autoconverter.Generator.Contexts;
using System;

namespace Paukertj.Autoconverter.Generator.Code
{
    internal static class ConversionGeneratorContextExtensions
    {
        internal static string GetGeneratorClassFullName<TContext>(this ConversionGeneratorContext<TContext> context)
            where TContext : TypeGeneratorContext
        {
            if (context == null || context.From == null || context.To == null)
            {
                throw new ArgumentNullException();
            }

            string from = context.From.PureFullName.Replace(".", string.Empty);
            string to = context.To.PureFullName.Replace(".", string.Empty);

            return $"{from}To{to}Converter";
        }
    }
}
