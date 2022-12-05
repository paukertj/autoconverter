using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition1;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCasesComposition1 : TestCasesCompositionBase<IBasicTestCasesService1, BasicTestCasesService1>
    {

    }

    public class BasicTestCasesService1 : IBasicTestCasesService1
    {
        private readonly IConvertingService _convertingService;

        public BasicTestCasesService1(IConvertingService convertingService)
        {
            _convertingService = convertingService;
        }

        public BasicToEntity1Scenario1 ConvertScenario1()
        {
            var from = GetSourceScenario1();
            
            return _convertingService.Convert<BasicFromEntity1Scenario1, BasicToEntity1Scenario1>(from);
        }

        public BasicFromEntity1Scenario1 GetSourceScenario1()
        {
            return new BasicFromEntity1Scenario1
            {
                Bool = true,
                Byte = 1,
                Char = 'A',
                Decimal = 1000,
                Integer = 10,
                Long = 100,
                String = "hello",
                UnsignedInteger = 11,
                UnsingnedLong = 111
            };
        }

        public BasicToEntity1Scenario2 ConvertScenario2()
        {
            var from = GetSourceScenario2();

            return _convertingService.Convert<BasicFromEntity1Scenario2, BasicToEntity1Scenario2>(from);
        }

        public BasicFromEntity1Scenario2 GetSourceScenario2()
        {
            return new BasicFromEntity1Scenario2
            {
                Bool = true,
                Byte = 1,
                Char = 'A',
                Decimal = 1000,
                Integer = 10,
                Long = 100,
                String = "hello",
                UnsignedInteger = 11,
                UnsingnedLong = 111
            };
        }

        public BasicToEntity1Scenario3 ConvertScenario3()
        {
            var from = GetSourceScenario3();

            return _convertingService.Convert<BasicFromEntity1Scenario3, BasicToEntity1Scenario3>(from);
        }

        public BasicFromEntity1Scenario3 GetSourceScenario3()
        {
            return new BasicFromEntity1Scenario3
            {
                Bool = true,
                Byte = 1,
                Char = 'A',
                Decimal = 1000,
                Integer = 10,
                Long = 100,
                String = "hello",
                UnsignedInteger = 11,
                UnsingnedLong = 111
            };
        }

        public BasicToEntity1Scenario4 ConvertScenario4()
        {
            var from = GetSourceScenario4();

            return _convertingService.Convert<BasicFromEntity1Scenario4, BasicToEntity1Scenario4>(from);
        }

        public BasicFromEntity1Scenario4 GetSourceScenario4()
        {
            return new BasicFromEntity1Scenario4
            {
                Bool = true,
                Byte = 1,
                Char = 'A',
                Decimal = 1000,
                Integer = 10,
                Long = 100,
                String = "hello",
                UnsignedInteger = 11,
                UnsingnedLong = 111
            };
        }

		public BasicToEntity1Scenario5 ConvertScenario5()
		{
			var from = GetSourceScenario5();

			return _convertingService.Convert<BasicFromEntity1Scenario5, BasicToEntity1Scenario5>(from);
		}

		public BasicFromEntity1Scenario5 GetSourceScenario5()
		{
			return new BasicFromEntity1Scenario5
			{
				Bool = true,
				Byte = 1,
				Char = 'A',
				Decimal = 1000,
				Integer = 10,
				Long = 100,
				String = "hello",
				UnsignedInteger = 11,
				UnsingnedLong = 111
			};
		}
	}
}
