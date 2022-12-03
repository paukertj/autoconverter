using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.CustomConverter.Composition1;
using Paukertj.Autoconverter.Generator.Tests.TestCases.CustomConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.InvalidTypeConversion
{
    [TestFixture]
    [Description("Type mismatch tests")]
    public class InvalidTypeConversionTestCases1 : TestCasesBase<ICustomConverterTestCaseService1, CustomConverterTestCasesComposition1>
    {
        public InvalidTypeConversionTestCases1() : base(@".\TestCases\InvalidTypeConversion\InvalidTypeConversionTestCasesComposition1.cs")
        { }

        [Test]
        public void CustomConverterTestCasesComposition1Scenario1()
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
