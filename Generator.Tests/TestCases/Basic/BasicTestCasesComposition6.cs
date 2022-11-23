using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition6;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCasesComposition6 : TestCasesCompositionBase<IBasicTestCasesService6, BasicTestCasesService6>
    {

    }

    public class BasicTestCasesService6 : IBasicTestCasesService6
    {
        private readonly IConvertingService _convertingService;
        private readonly Guid _id;

        public BasicTestCasesService6(IConvertingService convertingService)
        {
            _convertingService = convertingService;
            _id = Guid.NewGuid();
        }

        public BasicFromEntity6Scenario1 GetSourceScenario1()
        {
            return new BasicFromEntity6Scenario1
            {
                FirstName = "John",
                LastName = "Smith"
            };
        }

        public BasicToEntity6Scenario1 ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<BasicFromEntity6Scenario1, BasicToEntity6Scenario1>(from);
        }

        public BasicFromEntity6Scenario2 GetSourceScenario2()
        {
            return new BasicFromEntity6Scenario2
            {
                Id = _id,
                FirstName = "John",
                LastName = "Smith",
                Company = "My Company"
            };
        }

        public BasicToEntity6Scenario2 ConvertScenario2()
        {
            var from = GetSourceScenario2();

            return _convertingService.Convert<BasicFromEntity6Scenario2, BasicToEntity6Scenario2>(from);
        }
    }
}
