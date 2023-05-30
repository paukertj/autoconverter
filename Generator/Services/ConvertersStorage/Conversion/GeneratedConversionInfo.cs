using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Extensions;
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
            string from = GetConverterClassFragmentName(fromMember);

            string to = GetConverterClassFragmentName(toMember);

            return from + "To" + to + "Converter";
        }

        private static string GetConverterClassFragmentName(ConversionMember member)
        {
            string suffix = member.PureFullName == member.PureFullNameNullable
                ? string.Empty
                : "Nullable";

            string name = member.PureFullName
                .FirstUpperCase()
                .Replace(".", string.Empty)
                .ToString();
        
            return name + suffix;
        }
    }
}
