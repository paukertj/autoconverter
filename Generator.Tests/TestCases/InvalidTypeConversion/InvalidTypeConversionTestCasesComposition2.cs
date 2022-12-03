using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.InvalidTypeConversion.Composition1;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.InvalidTypeConversion
{
    public class InvalidTypeConversionTestCasesComposition2 : TestCasesCompositionBase<IInvalidTypeConversionTestCaseService2, InvalidTypeConversionTestCaseService2>
    {

    }

    public class InvalidTypeConversionTestCaseService2 : IInvalidTypeConversionTestCaseService2
    {
        private readonly IConvertingService _convertingService;

        public InvalidTypeConversionTestCaseService2(IConvertingService convertingService)
        {
            _convertingService = convertingService;
        }

        public InvalidTypeConversionToEntity2 ConvertScenario1()
        {
            var from = GetSourceScenario1();

            return _convertingService.Convert<InvalidTypeConversionFromEntity2, InvalidTypeConversionToEntity2>(from);
        }

        public InvalidTypeConversionFromEntity2 GetSourceScenario1()
        {
            return new InvalidTypeConversionFromEntity2
            {
                Property = null
            };
        }
    }
}
