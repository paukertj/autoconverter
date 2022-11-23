using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
	public class BasicTestCasesComposition4 : TestCasesCompositionBase<IBasicTestCasesService4, BasicTestCasesService4>
	{

	}

	public class BasicTestCasesService4 : IBasicTestCasesService4
	{
		private readonly IConvertingService _convertingService;

		public BasicTestCasesService4(IConvertingService convertingService)
		{
			_convertingService = convertingService;
		}

		public BasicToEntity4Scenario1 ConvertScenario1()
		{
			var from = GetSourceScenario1();

			return _convertingService.Convert<BasicFromEntity4Scenario1, BasicToEntity4Scenario1>(from);
		}

		public BasicFromEntity4Scenario1 GetSourceScenario1()
		{
			return new BasicFromEntity4Scenario1
			{
				PublicProperty = "hello"
			};
		}

		public BasicToEntity4Scenario2 ConvertScenario2()
		{
			var from = GetSourceScenario2();

			return _convertingService.Convert<BasicFromEntity4Scenario2, BasicToEntity4Scenario2>(from);
		}

		public BasicFromEntity4Scenario2 GetSourceScenario2()
		{
			return new BasicFromEntity4Scenario2();
		}

		public BasicToEntity4Scenario3 ConvertScenario3()
		{
			var from = GetSourceScenario3();

			return _convertingService.Convert<BasicFromEntity4Scenario3, BasicToEntity4Scenario3>(from);
		}

		public BasicFromEntity4Scenario3 GetSourceScenario3()
		{
			return new BasicFromEntity4Scenario3
			{
				PublicProperty = "hello"
			};
		}

		public BasicToEntity4Scenario4 ConvertScenario4()
		{
			var from = GetSourceScenario4();

			return _convertingService.Convert<BasicFromEntity4Scenario4, BasicToEntity4Scenario4>(from);
		}

		public BasicFromEntity4Scenario4 GetSourceScenario4()
		{
			return new BasicFromEntity4Scenario4();
		}

		public BasicToEntity4Scenario5 ConvertScenario5()
		{
			var from = GetSourceScenario5();

			return _convertingService.Convert<BasicFromEntity4Scenario5, BasicToEntity4Scenario5>(from);
		}

		public BasicFromEntity4Scenario5 GetSourceScenario5()
		{
			return new BasicFromEntity4Scenario5();
		}
	}
}
