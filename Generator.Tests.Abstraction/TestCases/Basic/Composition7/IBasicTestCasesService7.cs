using System;

namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition7
{
    public interface IBasicTestCasesService7
    {
        int? GetSourceScenario1();

        string ConvertScenario1();

        Guid? GetSourceScenario2();

        string ConvertScenario2();

        BasicFromEntity7Scenario3? GetSourceScenario3();

        BasicToEntity7Scenario3 ConvertScenario3();

        BasicFromEntity7Scenario4? GetSourceScenario4();

        BasicToEntity7Scenario4? ConvertScenario4();

        BasicFromEntity7Scenario5 GetSourceScenario5();

        BasicToEntity7Scenario5? ConvertScenario5();
    }
}
