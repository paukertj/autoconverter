using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.CustomConverter.Composition1
{
    public interface ICustomConverterTestCaseService1
    {
        CustomConverterFromEntity1 GetSourceScenario1();

        CustomConverterToEntity1 ConvertScenario1();

        List<CustomConverterFromEntity1> GetSourceScenario2();

        List<CustomConverterToEntity1> ConvertScenario2();
    }
}
