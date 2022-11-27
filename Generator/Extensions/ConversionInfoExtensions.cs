using Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion;

namespace Paukertj.Autoconverter.Generator.Extensions
{
    internal static class ConversionInfoExtensions
	{
		internal static string GetFileName(this ConversionInfoBase conversionInfo)
		{
			return conversionInfo.ImplementationName
				.GetFileName();
		}

        internal static string GetClassFullName(this ConversionInfoBase conversionInfo)
        {
            return conversionInfo.ImplementationNamespace + '.' + conversionInfo.ImplementationName;
        }
    }
}
