using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition1;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
	[TestFixture]
    [Description("Basic tests with different targets and sources")]
    public class BasicTestCases2 : TestCasesBase<IBasicTestCasesService2, BasicTestCasesComposition2>
	{
		public BasicTestCases2() : base(@".\TestCases\Basic\BasicTestCasesComposition2.cs")
		{ }

		[Test]
		public void BasicTestCasesComposition2Scenario1()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicFromEntity = service.GetSourceScenario1();

			var basicToEntity = service.ConvertScenario1();

			basicFromEntity.Decimal
				.Should()
				.Be(basicToEntity.Decimal);
			basicFromEntity.Long
				.Should()
				.Be(basicToEntity.Long);
			basicFromEntity.Integer
				.Should()
				.Be(basicToEntity.Integer);
			basicFromEntity.Byte
				.Should()
				.Be(basicToEntity.Byte);
			basicFromEntity.Bool
				.Should()
				.Be(basicToEntity.Bool);
			basicFromEntity.String
				.Should()
				.Be(basicToEntity.String);
		}

        [Test]
        public void BasicTestCasesComposition2Scenario2()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario2();

            var basicToEntity = service.ConvertScenario2();

            basicToEntity.FirstName
                .Should()
                .Be(basicFromEntity.FirstName);
            basicToEntity.LastName
                .Should()
                .Be(basicFromEntity.LastName);

            basicToEntity.Id
                .Should()
                .BeEmpty();
        }

        [Test]
        public void BasicTestCasesComposition2Scenario3()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario3();

            var basicToEntity = service.ConvertScenario3();

            basicToEntity.FirstName
                .Should()
                .Be(basicFromEntity.FirstName);
            basicToEntity.LastName
                .Should()
                .Be(basicFromEntity.LastName);
            basicToEntity.Id
                .Should()
                .Be(basicFromEntity.Id);
        }
    }
}
