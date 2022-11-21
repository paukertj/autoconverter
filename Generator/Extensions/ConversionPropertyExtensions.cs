using Paukertj.Autoconverter.Generator.Services.ConvertersStorage;

namespace Paukertj.Autoconverter.Generator.Extensions
{
	internal static class ConversionPropertyExtensions
	{
		internal static string GetTypeFullName(this ConversionProperty converisonProperty)
		{
			return converisonProperty.TypeSymbolForConversion.ToString();
		}
	}
}
