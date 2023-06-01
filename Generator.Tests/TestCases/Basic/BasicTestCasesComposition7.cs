using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition7;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCasesComposition7 : TestCasesCompositionBase<IBasicTestCasesService7, BasicTestCasesService7>
    {

    }

    public class BasicTestCasesService7 : IBasicTestCasesService7
    {
        private readonly IConvertingService _convertingService;

        public BasicTestCasesService7(IConvertingService convertingService)
        {
            _convertingService = convertingService;
        }

        public string ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<int?, string>(from);
        }

        public int? GetSourceScenario1()
        {
            return 1;
        }

        public string ConvertScenario2()
        {
            var from = GetSourceScenario2();

            return _convertingService.Convert<Guid?, string>(from);
        }

        public Guid? GetSourceScenario2()
        {
            return Guid.Empty;
        }

        public BasicToEntity7Scenario3 ConvertScenario3()
        {
            var from = GetSourceScenario3();

            return _convertingService.Convert<BasicFromEntity7Scenario3?, BasicToEntity7Scenario3>(from);
        }

        public BasicFromEntity7Scenario3? GetSourceScenario3()
        {
            return new BasicFromEntity7Scenario3
            {
                SomeValue = "some-value"
            };
        }

        public BasicToEntity7Scenario4? ConvertScenario4()
        {
            var from = GetSourceScenario4();

            return _convertingService.Convert<BasicFromEntity7Scenario4?, BasicToEntity7Scenario4?>(from);
        }

        public BasicFromEntity7Scenario4? GetSourceScenario4()
        {
            return null;
        }

        public BasicToEntity7Scenario5? ConvertScenario5()
        {
            var from = GetSourceScenario5();

            return _convertingService.Convert<BasicFromEntity7Scenario5, BasicToEntity7Scenario5?>(from);
        }

        public BasicFromEntity7Scenario5 GetSourceScenario5()
        {
            return new BasicFromEntity7Scenario5
            {
                SomeValue = "some-value"
            };
        }
    }
}
