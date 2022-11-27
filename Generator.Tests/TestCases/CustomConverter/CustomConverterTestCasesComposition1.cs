using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.CustomConverter.Composition1;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.CustomConverter
{
    public class CustomConverterTestCasesComposition1 : TestCasesCompositionBase<ICustomConverterTestCaseService1, CustomConverterTestCaseService1>
    {

    }

    public class CustomConverterTestCaseService1 : ICustomConverterTestCaseService1
    {
        private readonly IConvertingService _convertingService;

        public CustomConverterTestCaseService1(IConvertingService convertingService)
        {
            _convertingService = convertingService;
        }

        public CustomConverterToEntity1 ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<CustomConverterFromEntity1, CustomConverterToEntity1>(from);
		}

        public List<CustomConverterToEntity1> ConvertScenario2()
        {
            var from = GetSourceScenario2();

            return _convertingService.Convert<CustomConverterFromEntity1, CustomConverterToEntity1>(from) as List<CustomConverterToEntity1>;
        }

        public CustomConverterFromEntity1 GetSourceScenario1()
        {
            return new CustomConverterFromEntity1
            {
                Property11 = "1",
                Property21 = 2,
            };
        }

        public List<CustomConverterFromEntity1> GetSourceScenario2()
        {
            return new List<CustomConverterFromEntity1>
            {
                new CustomConverterFromEntity1
                {
                    Property11 = "11",
                    Property21 = 21,
                },
                new CustomConverterFromEntity1
                {
                    Property11 = "12",
                    Property21 = 22,
                }
            };
        }
	}

	internal class CustomConverter1 : IConverter<CustomConverterFromEntity1, CustomConverterToEntity1>
	{
		public CustomConverterToEntity1 Convert(CustomConverterFromEntity1 from)
		{
			return new CustomConverterToEntity1
			{
				Property12 = from.Property11,
				Property22 = from.Property21
			};
		}
	}
}