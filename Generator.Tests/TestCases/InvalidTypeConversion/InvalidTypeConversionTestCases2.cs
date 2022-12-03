using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.CustomConverter.Composition1;
using Paukertj.Autoconverter.Generator.Tests.TestCases.CustomConverter;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.InvalidTypeConversion
{
    [TestFixture]
    [Description("Type mismatch tests with nullable")]
    public class InvalidTypeConversionTestCases2 : TestCasesBase<ICustomConverterTestCaseService1, CustomConverterTestCasesComposition1>
    {
        public InvalidTypeConversionTestCases2() : base(@".\TestCases\InvalidTypeConversion\InvalidTypeConversionTestCasesComposition2.cs")
        { }

        [Test]
        public void CustomConverterTestCasesComposition2Scenario1()
        {
            Diagnostic.Count
                .Should()
                .Be(1);

            Diagnostic
                .First()
                .Id
                .Should()
                .Be("AC0006");
        }
    }
}
