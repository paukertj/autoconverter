using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition1;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCasesComposition2 : TestCasesCompositionBase<IBasicTestCasesService2, BasicTestCasesService2>
    {

    }

    public class BasicTestCasesService2 : IBasicTestCasesService2
    {
        private readonly IConvertingService _convertingService;
        private readonly Guid _id;

        public BasicTestCasesService2(IConvertingService convertingService)
        {
            _id = Guid.NewGuid();
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

        public BasicFromEntity2Scenario2 GetSourceScenario2()
        {
            return new BasicFromEntity2Scenario2
            {
                FirstName = "John",
                LastName = "Smith"
            };
        }

        public BasicToEntity2Scenario2 ConvertScenario2()
        {
            var from = GetSourceScenario2();

            return _convertingService.Convert<BasicFromEntity2Scenario2, BasicToEntity2Scenario2>(from);
        }

        public BasicFromEntity2Scenario3 GetSourceScenario3()
        {
            return new BasicFromEntity2Scenario3
            {
                Id = _id,
                FirstName = "John",
                LastName = "Smith",
                Company = "My Company"
            };
        }

        public BasicToEntity2Scenario3 ConvertScenario3()
        {
            var from = GetSourceScenario3();

            return _convertingService.Convert<BasicFromEntity2Scenario3, BasicToEntity2Scenario3>(from);
        }
    }
}
