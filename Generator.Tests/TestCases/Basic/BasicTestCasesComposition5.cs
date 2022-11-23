using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition5;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCasesComposition5 : TestCasesCompositionBase<IBasicTestCasesService5, BasicTestCasesService5>
    {

    }

    public class BasicTestCasesService5 : IBasicTestCasesService5
    {
        private readonly IConvertingService _convertingService;

        public BasicTestCasesService5(IConvertingService convertingService)
        {
            _convertingService = convertingService;
        }

        public BasicToEntity5Scenario1 ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<BasicFromEntity5Scenario1, BasicToEntity5Scenario1>(from);
        }

        public BasicFromEntity5Scenario1 GetSourceScenario1()
        {
            return new BasicFromEntity5Scenario1
            {
                Property = "hello"
            };
        }
    }
}
