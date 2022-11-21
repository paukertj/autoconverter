namespace Paukertj.Autoconverter.Generator.Services.StaticAnalysis
{
	public sealed class ConvertingServiceInfo
	{
		public string InterfaceName { get; }

		public string NamespaceName { get; }

		public ConvertingServiceInfo(string interfaceName, string namespaceName)
		{
			InterfaceName = interfaceName;
			NamespaceName = namespaceName;
		}
	}
}
