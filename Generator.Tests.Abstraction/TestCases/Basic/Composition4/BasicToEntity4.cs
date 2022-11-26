namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3
{
    public class BasicToEntity4Scenario1
    {
		public string PublicProperty { get; set; }

		private string _privateProperty { get; set; }

		private string _privateField;

		private readonly string _privateReadOnlyField2;
	}

	public class BasicToEntity4Scenario2
	{
		private string _privateProperty { get; set; }

		private string _privateField;

		private readonly string _privateReadOnlyField2;
	}

	public class BasicToEntity4Scenario3
	{
		public string PublicProperty { get; init; }
	}

	public class BasicToEntity4Scenario4
	{
		public string PublicProperty { get; }
	}

	public class BasicToEntity4Scenario5
	{
		public string PublicProperty { get; private set; }
	}
}
