namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3
{
    public interface IBasicTestCasesService3
    {
        BasicFromEntity3 GetSourceScenario1();

        BasicFromEntity3 GetSourceScenario2();

        BasicFromEntity3 GetSourceScenario3();

        BasicToEntity3 ConvertScenario1();

        BasicToEntity3 ConvertScenario2();

        BasicToEntity3 ConvertScenario3();
    }
}
