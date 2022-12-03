using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.InvalidTypeConversion.Composition1;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.InvalidTypeConversion
{
    public class InvalidTypeConversionTestCasesComposition1 : TestCasesCompositionBase<IInvalidTypeConversionTestCaseService1, InvalidTypeConversionTestCaseService1>
    {

    }

    public class InvalidTypeConversionTestCaseService1 : IInvalidTypeConversionTestCaseService1
    {
        private readonly IConvertingService _convertingService;

        public InvalidTypeConversionTestCaseService1(IConvertingService convertingService)
        {
            _convertingService = convertingService;
        }

        public InvalidTypeConversionToEntity1 ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<InvalidTypeConversionFromEntity1, InvalidTypeConversionToEntity1>(from);
        }

        public InvalidTypeConversionFromEntity1 GetSourceScenario1()
        {
            return new InvalidTypeConversionFromEntity1
            {
                Property = "abc"
            };
        }
    }
}
