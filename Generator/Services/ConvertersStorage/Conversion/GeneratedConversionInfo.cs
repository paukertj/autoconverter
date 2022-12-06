using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion
{
    public sealed class GeneratedConversionInfo : ConversionInfoBase
    {
        public ConversionMember From { get; }

        public ConversionMember To { get; }
        
        public override string ImplementationNamespace { get; }

        public override string ImplementationName { get; }

        public bool RequireConversion => From.Properties
            .Any(p => p.RequireConversion) && To.Properties.Any(p => p.RequireConversion);

        public GeneratedConversionInfo(ConversionMember from, ConversionMember to, string implementationNamespace) : base(from?.FullName, to?.FullName)
        {
            if (from == null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            if (to == null)
            {
                throw new ArgumentNullException(nameof(to));
            }

            From = from;
            To = to;

            ImplementationNamespace = implementationNamespace;
            ImplementationName = GetClassName(from, to);
        }

        private static string GetClassName(ConversionMember fromMember, ConversionMember toMember)
        {
            string from = fromMember.FullName
                .Replace(".", string.Empty)
                .ToString();

            string to = toMember.FullName
                .Replace(".", string.Empty)
                .ToString();

            return from + "To" + to + "Converter";
        }
    }
}
