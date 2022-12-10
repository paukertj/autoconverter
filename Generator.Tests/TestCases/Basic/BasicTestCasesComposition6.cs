using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition5;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition6;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public BasicToEntity6Scenario1 ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<BasicFromEntity6Scenario1, BasicToEntity6Scenario1>(from);

        }

        public BasicToEntity6Scenario2 ConvertScenario2()
        {
            var from = GetSourceScenario2();

            return _convertingService.Convert<BasicFromEntity6Scenario2, BasicToEntity6Scenario2>(from);
        }

        public BasicToEntity6Scenario3 ConvertScenario3()
        {
            var from = GetSourceScenario3();

            return _convertingService.Convert<BasicFromEntity6Scenario3, BasicToEntity6Scenario3>(from);
        }

        public BasicToEntity6Scenario4 ConvertScenario4()
        {
            var from = GetSourceScenario4();

            return _convertingService.Convert<BasicFromEntity6Scenario4, BasicToEntity6Scenario4>(from);
        }

        public BasicToEntity6Scenario5 ConvertScenario5()
        {
            var from = GetSourceScenario5();

            return _convertingService.Convert<BasicFromEntity6Scenario5, BasicToEntity6Scenario5>(from);
        }

        public BasicFromEntity6Scenario1 GetSourceScenario1()
        {
            return new BasicFromEntity6Scenario1
            {
                Id = _id
            };
        }

        public BasicFromEntity6Scenario2 GetSourceScenario2()
        {
            return new BasicFromEntity6Scenario2
            {
                Id = _id
            };
        }

        public BasicFromEntity6Scenario3 GetSourceScenario3()
        {
            return new BasicFromEntity6Scenario3
            {
                Id = _id
            };
        }

        public BasicFromEntity6Scenario4 GetSourceScenario4()
        {
            return new BasicFromEntity6Scenario4
            {
                Id = _id
            };
        }

        public BasicFromEntity6Scenario5 GetSourceScenario5()
        {
            return new BasicFromEntity6Scenario5
            {
                Id = "Id"
            };
        }
    }
}
