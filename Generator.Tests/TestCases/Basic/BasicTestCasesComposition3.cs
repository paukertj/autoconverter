using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
	public class BasicTestCasesComposition3 : TestCasesCompositionBase<IBasicTestCasesService3, BasicTestCasesService3>
	{

	}

	public class BasicTestCasesService3 : IBasicTestCasesService3
	{
		private readonly IConvertingService _convertingService;

		public BasicTestCasesService3(IConvertingService convertingService)
		{
			_convertingService = convertingService;
		}

		public BasicToEntity3Scenario123 ConvertScenario1()
		{
			var from = GetSourceScenario1();

			return _convertingService.Convert<BasicFromEntity3Scenario123, BasicToEntity3Scenario123>(from);
		}

		public BasicToEntity3Scenario123 ConvertScenario2()
		{
			var from = GetSourceScenario2();

            return _convertingService.Convert<BasicFromEntity3Scenario123, BasicToEntity3Scenario123>(from);
        }

		public BasicToEntity3Scenario123 ConvertScenario3()
		{
			var from = GetSourceScenario3();

            return _convertingService.Convert<BasicFromEntity3Scenario123, BasicToEntity3Scenario123>(from);
        }

		public BasicToEntity3Scenario4 ConvertScenario4()
		{
			var from = GetSourceScenario4();

			return _convertingService.Convert<BasicFromEntity3Scenario4, BasicToEntity3Scenario4>(from);
		}

		public BasicFromEntity3Scenario123 GetSourceScenario1()
		{
			return new BasicFromEntity3Scenario123
			{
				Level1 = new BasicFromEntity3Scenario123.BasicFromEntity31
				{
					Level2 = new BasicFromEntity3Scenario123.BasicFromEntity31.BasicFromEntity32
					{
						Level3 = new BasicFromEntity3Scenario123.BasicFromEntity31.BasicFromEntity32.BasicFromEntity33
						{
							Level4 = "end"
						}
					}
				}
			};
		}

		public BasicFromEntity3Scenario123 GetSourceScenario2()
		{
			return new BasicFromEntity3Scenario123
			{
				Level1 = new BasicFromEntity3Scenario123.BasicFromEntity31
				{
					Level2 = new BasicFromEntity3Scenario123.BasicFromEntity31.BasicFromEntity32
					{
						Level3 = null
					}
				}
			};
		}

		public BasicFromEntity3Scenario123 GetSourceScenario3()
		{
			return new BasicFromEntity3Scenario123
			{
				Level1 = null
			};
		}

		public BasicFromEntity3Scenario4 GetSourceScenario4()
		{
			return new BasicFromEntity3Scenario4
			{
				Level1 = new List<BasicFromEntity3Scenario4.BasicFromEntity31>
				{
					new BasicFromEntity3Scenario4.BasicFromEntity31
					{
						Level21 = new DateTimeOffset(2020, 01, 01, 01, 01, 01, new TimeSpan()),
						Level22 = new DateTimeOffset(2021, 01, 01, 01, 01, 01, new TimeSpan()),
					},
					new BasicFromEntity3Scenario4.BasicFromEntity31
					{
						Level21 = new DateTimeOffset(2022, 01, 01, 01, 01, 01, new TimeSpan()),
						Level22 = new DateTimeOffset(2023, 01, 01, 01, 01, 01, new TimeSpan()),
					}
				}
			};
		}
	}
}
