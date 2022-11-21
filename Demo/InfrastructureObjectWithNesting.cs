using static Paukertj.Autoconverter.Demo.DomainObjectWithNesting;

namespace Paukertj.Autoconverter.Demo
{
	public class InfrastructureObjectWithNesting
	{
		public string StringProperty { get; init; }

		public IList<string> ListOfStrings { get; init; }

		public IList<NestedInfrastructure> ListOfTypes { get; init; }

		//public string StringProperty2 { get; init; }

		public NestedInfrastructure Nested { get; init; }

		public class NestedInfrastructure
		{
			public int IntProperty { get; init; }
		}
	}
}
