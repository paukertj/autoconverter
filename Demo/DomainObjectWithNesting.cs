namespace Paukertj.Autoconverter.Demo
{
	public class DomainObjectWithNesting
	{
		public string StringProperty { get; init; }

		public IList<string> ListOfStrings { get; init; }

		public IList<NestedDomain> ListOfTypes { get; init; }

		public NestedDomain Nested { get; init; }

		public class NestedDomain
		{
			public int IntProperty { get; init; }
		}
	}
}
