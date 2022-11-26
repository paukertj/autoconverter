using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition5;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCases5 : TestCasesBase<IBasicTestCasesService5, BasicTestCasesComposition5>
    {
        public BasicTestCases5() : base(@".\TestCases\Basic\BasicTestCasesComposition5.cs")
        { }

        [Test]
        public void BasicTestCasesComposition5Scenario1()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario1();

            var basicToEntity = service.ConvertScenario1();

            basicToEntity.Property
                .Should()
                .Be(basicFromEntity.Property);
        }
    }
}
