namespace Paukertj.Autoconverter.Generator.Services.StaticAnalysis
{
	public sealed class ConverterServiceInfo
	{
		public string InterfaceName { get; }

		public string NamespaceName { get; }

		public ConverterServiceInfo(string interfaceName, string namespaceName)
		{
			InterfaceName = interfaceName;
			NamespaceName = namespaceName;
		}
	}
}
