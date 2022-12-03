using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.InvalidTypeConversion.Composition1
{
    public interface IInvalidTypeConversionTestCaseService1
    {
        InvalidTypeConversionToEntity1 ConvertScenario1();

        InvalidTypeConversionFromEntity1 GetSourceScenario1();
    }
}
