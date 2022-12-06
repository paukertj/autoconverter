namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition1
{
    public interface IBasicTestCasesService1
    {
        BasicFromEntity1Scenario1 GetSourceScenario1();

        BasicToEntity1Scenario1 ConvertScenario1();

        BasicFromEntity1Scenario2 GetSourceScenario2();

        BasicToEntity1Scenario2 ConvertScenario2();

        BasicFromEntity1Scenario3 GetSourceScenario3();

		BasicFromEntity1Scenario5 GetSourceScenario5();

		BasicToEntity1Scenario3 ConvertScenario3();

        BasicFromEntity1Scenario4 GetSourceScenario4();

        BasicToEntity1Scenario4 ConvertScenario4();

		BasicToEntity1Scenario5 ConvertScenario5();
	}
}
