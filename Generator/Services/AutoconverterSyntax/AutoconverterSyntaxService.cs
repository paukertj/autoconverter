using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Code;
using Paukertj.Autoconverter.Generator.Contexts;
using Paukertj.Autoconverter.Generator.Entities;
using Paukertj.Autoconverter.Generator.Services.ConversionLogic;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;

namespace Paukertj.Autoconverter.Generator.Services.AutoconverterSyntax
{
    internal class AutoconverterSyntaxService : IAutoconverterSyntaxService
    {
        private readonly IConversionLogicService _conversionLogicService;
        private readonly ISemanticAnalysisService _semanticAnalysisService;

        public AutoconverterSyntaxService(IConversionLogicService conversionLogicService, ISemanticAnalysisService semanticAnalysisService)
        {
            _conversionLogicService = conversionLogicService;
            _semanticAnalysisService = semanticAnalysisService;
        }

        public StatementSyntax GenerateServiceRegistrationStatementFromConversion(TypeConversion conversion)
        {
            if (_conversionLogicService.ConverterExists(conversion.From, conversion.To))
            {
                return null;
            }

            var fromTypeGeneratorContext = _semanticAnalysisService.TypeSyntaxToTypeGeneratorContext(conversion.From);
            var toTypeGeneratorContext = _semanticAnalysisService.TypeSyntaxToTypeGeneratorContext(conversion.To);

            _conversionLogicService.SetConverterAsExists(conversion.From, conversion.To);

            var typeGeneratorContext = new ConversionGeneratorContext<TypeGeneratorContext>(fromTypeGeneratorContext, toTypeGeneratorContext);

            return AutoconverterSyntaxFactory.GeneratorDepenedcyInjectionRegistration(typeGeneratorContext);
        }
    }
}
