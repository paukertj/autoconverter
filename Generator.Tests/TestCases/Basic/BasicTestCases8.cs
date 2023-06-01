using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition8;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    [TestFixture]
    [Description("Basic tests for enums")]
    public class BasicTestCases8 : TestCasesBase<IBasicTestCasesService8, BasicTestCasesComposition8>
    {
        public BasicTestCases8() : base(@".\TestCases\Basic\BasicTestCasesComposition8.cs")
        { }

        [Test]
        public void BasicTestCasesComposition8cenario1()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario1();

            var basicToEntity = service.ConvertScenario1();

            ((int)basicToEntity)
                .Should()
                .Be((int)basicFromEntity);
        }
    }
}
