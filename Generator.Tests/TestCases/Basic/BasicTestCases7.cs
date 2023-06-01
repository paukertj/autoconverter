using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition7;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    [TestFixture]
    [Description("Basic tests for nullable primitive types and structures")]
    public class BasicTestCases7 : TestCasesBase<IBasicTestCasesService7, BasicTestCasesComposition7>
    {
        public BasicTestCases7() : base(@".\TestCases\Basic\BasicTestCasesComposition7.cs")
        { }

        [Test]
        public void BasicTestCasesComposition7Scenario1()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario1();

            var basicToEntity = service.ConvertScenario1();

            basicToEntity
                .Should()
                .Be(basicFromEntity?.ToString());
        }

        [Test]
        public void BasicTestCasesComposition7Scenario2()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario2();

            var basicToEntity = service.ConvertScenario2();

            basicToEntity
                .Should()
                .Be(basicFromEntity?.ToString());
        }

        [Test]
        public void BasicTestCasesComposition7Scenario3()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario3();

            var basicToEntity = service.ConvertScenario3();

            basicToEntity
                .SomeValue
                .Should()
                .Be(basicFromEntity.Value.SomeValue);
        }
        
        [Test]
        public void BasicTestCasesComposition7Scenario4()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario4();

            var basicToEntity = service.ConvertScenario4();

            basicFromEntity?
                .Should()
                .Be(null);

            basicToEntity?
                .Should()
                .Be(basicFromEntity);
        }

        [Test]
        public void BasicTestCasesComposition7Scenario5()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario5();

            var basicToEntity = service.ConvertScenario5();

            basicToEntity?
                .Should()
                .NotBe(null);

            basicToEntity
                .Value
                .SomeValue
                .Should()
                .Be(basicFromEntity.SomeValue);
        }
    }
}