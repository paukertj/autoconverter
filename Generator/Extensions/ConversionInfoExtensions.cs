using Paukertj.Autoconverter.Generator.Services.ConvertersStorage;

namespace Paukertj.Autoconverter.Generator.Extensions
{
	internal static class ConversionInfoExtensions
	{
		internal static string GetFileName(this ConversionInfo conversionInfo)
		{
			return conversionInfo
				.GetClassName()
				.GetFileName();
		}

		internal static string GetClassName(this ConversionInfo conversionInfo)
		{
			string from = conversionInfo.From.FullName
				.Replace(".", string.Empty)
				.ToString();

			string to = conversionInfo.From.FullName
				.Replace(".", string.Empty)
				.ToString();

			return from + "To" + to + "Converter";
		}
	}
}
