using Microsoft.CodeAnalysis;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage
{
	public sealed class ConversionProperty
	{
		public IPropertySymbol PropertySymbol { get; }

		public ITypeSymbol TypeSymbolForConversion { get; private set; }

		public bool RequireConversion { get; private set; }

		public ConversionProperty(IPropertySymbol propertySymbol)
		{
			PropertySymbol = propertySymbol;
			TypeSymbolForConversion = propertySymbol.Type;
		}

		public ConversionProperty(IPropertySymbol propertySymbol, ITypeSymbol typeSymbolForConversion)
		{
			PropertySymbol = propertySymbol;
			TypeSymbolForConversion = typeSymbolForConversion;
		}

		public void WillRequireConversion()
		{
			RequireConversion = true;
		}

		public void ChangeConversionType(ITypeSymbol typeSymbolForConversion)
		{
			TypeSymbolForConversion = typeSymbolForConversion;
		}
	}
}
