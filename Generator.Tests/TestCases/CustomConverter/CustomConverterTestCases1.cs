using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.CustomConverter.Composition1;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.CustomConverter
{
    [TestFixture]
    [Description("Basic custom converter tests")]
    public class CustomConverterTestCases1 : TestCasesBase<ICustomConverterTestCaseService1, CustomConverterTestCasesComposition1>
    {
        public CustomConverterTestCases1() : base(@".\TestCases\CustomConverter\CustomConverterTestCasesComposition1.cs")
        { }

        [Test]
        public void CustomConverterTestCasesComposition1Scenario1()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario1();

            var basicToEntity = service.ConvertScenario1();

            basicFromEntity.Property11
                .Should()
                .Be(basicToEntity.Property12);
            basicFromEntity.Property21
                .Should()
                .Be(basicToEntity.Property22);
        }

        [Test]
        public void CustomConverterTestCasesComposition1Scenario2()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario2();

            var basicToEntity = service.ConvertScenario2();

            basicFromEntity.Count
                .Should()
                .Be(basicToEntity.Count);

            for (int i = 0; i < basicToEntity.Count; i++)
            {
                basicFromEntity[i].Property11
                    .Should()
                    .Be(basicToEntity[i].Property12);
                basicFromEntity[i].Property21
                    .Should()
                    .Be(basicToEntity[i].Property22);
            }
        }
    }
}
