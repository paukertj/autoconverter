namespace Paukertj.Autoconverter.Generator.Services.StaticAnalysis
{
	public sealed class EntryPointInfo
	{
		public string MethodName { get; }

		public string ClassName { get; }

		public string NamespaceName { get; }

		public EntryPointInfo(string methodName, string className, string namespaceName)
		{
			MethodName = methodName;
			ClassName = className;
			NamespaceName = namespaceName;
		}
	}
}
