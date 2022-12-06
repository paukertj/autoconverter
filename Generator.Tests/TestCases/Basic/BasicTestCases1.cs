using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition1;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    [TestFixture]
    [Description("Basic mapping tests")]
	public class BasicTestCases1 : TestCasesBase<IBasicTestCasesService1, BasicTestCasesComposition1>
	{
		public BasicTestCases1() : base(@".\TestCases\Basic\BasicTestCasesComposition1.cs")
		{ }

		[Test]
		public void BasicTestCasesComposition1Scenario1()
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
			basicFromEntity.UnsingnedLong
				.Should()
				.Be(basicToEntity.UnsingnedLong);
			basicFromEntity.Long
				.Should()
				.Be(basicToEntity.Long);
			basicFromEntity.UnsignedInteger
				.Should()
				.Be(basicToEntity.UnsignedInteger);
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
			basicFromEntity.Char
				.Should()
				.Be(basicToEntity.Char);
		}

        [Test]
        public void BasicTestCasesComposition1Scenario2()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario2();

            var basicToEntity = service.ConvertScenario2();

            basicFromEntity.Decimal
                .Should()
                .Be(basicToEntity.Decimal);
            basicFromEntity.UnsingnedLong
                .Should()
                .Be(basicToEntity.UnsingnedLong);
            basicFromEntity.Long
                .Should()
                .Be(basicToEntity.Long);
            basicFromEntity.UnsignedInteger
                .Should()
                .Be(basicToEntity.UnsignedInteger);
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
            basicFromEntity.Char
                .Should()
                .Be(basicToEntity.Char);
        }

        [Test]
        public void BasicTestCasesComposition1Scenario3()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario3();

            var basicToEntity = service.ConvertScenario3();

            basicFromEntity.Decimal
                .Should()
                .Be(basicToEntity.Decimal);
            basicFromEntity.UnsingnedLong
                .Should()
                .Be(basicToEntity.UnsingnedLong);
            basicFromEntity.Long
                .Should()
                .Be(basicToEntity.Long);
            basicFromEntity.UnsignedInteger
                .Should()
                .Be(basicToEntity.UnsignedInteger);
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
            basicFromEntity.Char
                .Should()
                .Be(basicToEntity.Char);
        }

        [Test]
        public void BasicTestCasesComposition1Scenario4()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario4();

            var basicToEntity = service.ConvertScenario4();

            basicFromEntity.Decimal
                .Should()
                .Be(basicToEntity.Decimal);
            basicFromEntity.UnsingnedLong
                .Should()
                .Be(basicToEntity.UnsingnedLong);
            basicFromEntity.Long
                .Should()
                .Be(basicToEntity.Long);
            basicFromEntity.UnsignedInteger
                .Should()
                .Be(basicToEntity.UnsignedInteger);
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
            basicFromEntity.Char
                .Should()
                .Be(basicToEntity.Char);
        }

		[Test]
		public void BasicTestCasesComposition1Scenario5()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicFromEntity = service.GetSourceScenario5();

			var basicToEntity = service.ConvertScenario5();

			basicFromEntity.Decimal
				.Should()
				.Be(basicToEntity.Decimal);
			basicFromEntity.UnsingnedLong
				.Should()
				.Be(basicToEntity.UnsingnedLong);
			basicFromEntity.Long
				.Should()
				.Be(basicToEntity.Long);
			basicFromEntity.UnsignedInteger
				.Should()
				.Be(basicToEntity.UnsignedInteger);
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
			basicFromEntity.Char
				.Should()
				.Be(basicToEntity.Char);
		}
	}
}
