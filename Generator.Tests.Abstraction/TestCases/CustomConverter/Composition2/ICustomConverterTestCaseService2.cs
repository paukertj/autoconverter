using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.CustomConverter.Composition1
{
    public interface ICustomConverterTestCaseService2
    {
        CustomConverterFromEntity2 GetSourceScenario1();

        CustomConverterToEntity2 ConvertScenario1();

        List<CustomConverterFromEntity2> GetSourceScenario2();

        List<CustomConverterToEntity2> ConvertScenario2();
    }
}
