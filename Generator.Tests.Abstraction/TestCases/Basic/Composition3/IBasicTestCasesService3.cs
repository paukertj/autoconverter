namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3
{
    public interface IBasicTestCasesService3
    {
        BasicFromEntity3Scenario123 GetSourceScenario1();

        BasicFromEntity3Scenario123 GetSourceScenario2();

        BasicFromEntity3Scenario123 GetSourceScenario3();

		BasicFromEntity3Scenario4 GetSourceScenario4();

		BasicToEntity3Scenario123 ConvertScenario1();

        BasicToEntity3Scenario123 ConvertScenario2();

        BasicToEntity3Scenario123 ConvertScenario3();

		BasicToEntity3Scenario4 ConvertScenario4();
	}
}
