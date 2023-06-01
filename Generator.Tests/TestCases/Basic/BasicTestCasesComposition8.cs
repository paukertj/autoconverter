using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition8;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCasesComposition8 : TestCasesCompositionBase<IBasicTestCasesService8, BasicTestCasesService8>
    {

    }

    public class BasicTestCasesService8 : IBasicTestCasesService8
    {
        private readonly IConvertingService _convertingService;

        public BasicTestCasesService8(IConvertingService convertingService)
        {
            _convertingService = convertingService;
        }

        public BasicFromEntity8Scenario1 ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<BasicToEntity8Scenario1, BasicFromEntity8Scenario1>(from);
        }

        public BasicToEntity8Scenario1 GetSourceScenario1()
        {
            return BasicToEntity8Scenario1.Value1;
        }
    }
}
