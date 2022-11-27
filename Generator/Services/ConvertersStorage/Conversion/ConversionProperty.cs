using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion
{
    public sealed class ConversionProperty
    {
        public IPropertySymbol PropertySymbol { get; }

        public ITypeSymbol TypeSymbolForConversion { get; private set; }

        public bool RequireConversion { get; private set; }

        public IReadOnlyList<string> IgnoredForConverionToTypes => _ignoredForConverionToTypes;
        private List<string> _ignoredForConverionToTypes = new List<string>();

        public ConversionProperty(IPropertySymbol propertySymbol)
        {
            PropertySymbol = propertySymbol;
            TypeSymbolForConversion = propertySymbol.Type;
        }

        public void WillRequireConversion()
        {
            RequireConversion = true;
        }

        public void ChangeConversionType(ITypeSymbol typeSymbolForConversion)
        {
            TypeSymbolForConversion = typeSymbolForConversion;
        }

        public void IgnoreForConverionToTypes(params string[] typeSymbolForConversion)
        {
            _ignoredForConverionToTypes.AddRange(typeSymbolForConversion);
        }
    }
}
