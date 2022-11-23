using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition1;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCasesComposition2 : TestCasesCompositionBase<IBasicTestCasesService2, BasicTestCasesService2>
    {

    }

    public class BasicTestCasesService2 : IBasicTestCasesService2
    {
        private readonly IConvertingService _convertingService;

        public BasicTestCasesService2(IConvertingService convertingService)
        {
            _convertingService = convertingService;
        }

        public BasicToEntity2Scenario1 ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<BasicFromEntity2Scenario1, BasicToEntity2Scenario1>(from);
        }

        public BasicFromEntity2Scenario1 GetSourceScenario1()
        {
            return new BasicFromEntity2Scenario1
            {
                Bool = true,
                Byte = 1,
                Decimal = 1000,
                Integer = 10,
                Long = 100,
                String = "hello",
            };
        }
    }
}
