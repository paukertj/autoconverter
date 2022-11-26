using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
	[TestFixture]
    [Description("Basic access modifiers tests")]
    public class BasicTestCases4 : TestCasesBase<IBasicTestCasesService4, BasicTestCasesComposition4>
	{
		public BasicTestCases4() : base(@".\TestCases\Basic\BasicTestCasesComposition4.cs")
		{ }

		[Test]
		public void BasicTestCasesComposition4Scenario1()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicFromEntity = service.GetSourceScenario1();

			var basicToEntity = service.ConvertScenario1();

			basicToEntity.PublicProperty
				.Should()
				.Be(basicFromEntity.PublicProperty);
		}

		[Test]
		public void BasicTestCasesComposition4Scenario2()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicToEntity = service.ConvertScenario2();

			basicToEntity
				.Should()
				.NotBeNull();
		}

		[Test]
		public void BasicTestCasesComposition4Scenario3()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicFromEntity = service.GetSourceScenario3();

			var basicToEntity = service.ConvertScenario3();

			basicToEntity.PublicProperty
				.Should()
				.Be(basicFromEntity.PublicProperty);
		}

		[Test]
		public void BasicTestCasesComposition4Scenario4()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicToEntity = service.ConvertScenario4();

			basicToEntity
				.Should()
				.NotBeNull();
		}

		[Test]
		public void BasicTestCasesComposition4Scenario5()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicToEntity = service.ConvertScenario5();

			basicToEntity
				.Should()
				.NotBeNull();
		}
	}
}
