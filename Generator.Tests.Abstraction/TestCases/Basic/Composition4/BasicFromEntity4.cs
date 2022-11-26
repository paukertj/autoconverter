namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3
{
    public class BasicFromEntity4Scenario1
	{
        public string PublicProperty { get; set; }

		private string _privateProperty { get; set; }

        private string _privateField;

        private readonly string _privateReadOnlyField2;
    }

	public class BasicFromEntity4Scenario2
	{
		private string _privateProperty { get; set; }

		private string _privateField;

		private readonly string _privateReadOnlyField2;
	}

	public class BasicFromEntity4Scenario3
	{
		public string PublicProperty { get; init; }
	}

	public class BasicFromEntity4Scenario4
	{
		public string PublicProperty { get; }
	}

	public class BasicFromEntity4Scenario5
	{
		public string PublicProperty { get; private set; }
	}
}
