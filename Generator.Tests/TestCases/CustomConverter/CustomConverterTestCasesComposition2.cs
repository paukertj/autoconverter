using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.CustomConverter.Composition1;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.CustomConverter
{
	public class CustomConverterTestCasesComposition2 : TestCasesCompositionBase<ICustomConverterTestCaseService2, CustomConverterTestCaseService2>
	{

	}

	public class CustomConverterTestCaseService2 : ICustomConverterTestCaseService2
	{
		private readonly IConvertingService _convertingService;

		public CustomConverterTestCaseService2(IConvertingService convertingService)
		{
			_convertingService = convertingService;
		}

		public CustomConverterToEntity2 ConvertScenario1()
		{
			var from = GetSourceScenario1();

			return _convertingService.Convert<CustomConverterFromEntity2, CustomConverterToEntity2>(from);
		}

		public List<CustomConverterToEntity2> ConvertScenario2()
		{
			var from = GetSourceScenario2();

			return _convertingService.Convert<CustomConverterFromEntity2, CustomConverterToEntity2>(from) as List<CustomConverterToEntity2>;
		}

		public CustomConverterFromEntity2 GetSourceScenario1()
		{
			return new CustomConverterFromEntity2
			{
				Property11 = "1",
				Property21 = 2,
			};
		}

		public List<CustomConverterFromEntity2> GetSourceScenario2()
		{
			return new List<CustomConverterFromEntity2>
			{
				new CustomConverterFromEntity2
				{
					Property11 = "11",
					Property21 = 21,
				},
				new CustomConverterFromEntity2
				{
					Property11 = "12",
					Property21 = 22,
				}
			};
		}

		internal class CustomConverter2 : IConverter<CustomConverterFromEntity2, CustomConverterToEntity2>
		{
			public CustomConverterToEntity2 Convert(CustomConverterFromEntity2 from)
			{
				return new CustomConverterToEntity2
				{
					Property12 = from.Property11,
					Property22 = from.Property21
				};
			}
		}
	}
}
