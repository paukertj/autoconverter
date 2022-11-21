namespace Paukertj.Autoconverter.Generator.Services.StaticAnalysis
{
	internal interface IStaticAnalysisService
	{
		EntryPointInfo GetEntryPointInfo();

		ConvertingServiceInfo GetConvertingServiceInfo();

		ConverterServiceInfo GetConverterServiceInfo();
	}
}
