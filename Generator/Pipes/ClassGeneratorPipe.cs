using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Code;
using Paukertj.Autoconverter.Generator.Contexts;
using Paukertj.Autoconverter.Generator.Entities;
using Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes;
using Paukertj.Autoconverter.Generator.Services.AutoconverterSyntax;
using Paukertj.Autoconverter.Generator.Services.ConversionLogic;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal sealed class ClassGeneratorPipe : IGeneratorConverter, IGeneratorDependencyInjectionRegistering
    {
        private readonly IConversionLogicService _conversionLogicService;
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly IAutoconverterSyntaxService _autoconverterSyntaxService;
        private readonly ISyntaxNodesRepository<GenericNameSyntax> _syntaxNodesRepository;
        private readonly List<StatementSyntax> _dependencyInjectionRegistrations;

        public ClassGeneratorPipe(
            IConversionLogicService conversionLogicService,
            ISemanticAnalysisService semanticAnalysisService,
            IAutoconverterSyntaxService autoconverterSyntaxService,
            ISyntaxNodesRepository<GenericNameSyntax> syntaxNodesRepository)
        {
            _conversionLogicService = conversionLogicService;
            _semanticAnalysisService = semanticAnalysisService;
            _autoconverterSyntaxService = autoconverterSyntaxService;
            _syntaxNodesRepository = syntaxNodesRepository;

            _dependencyInjectionRegistrations = new List<StatementSyntax>();
        }

        public IEnumerable<TypeConversion> GetData()
        {
            return _syntaxNodesRepository
                .Where(n => n.Identifier.ValueText == nameof(IConvertingService.Convert))
                .Select(_semanticAnalysisService.GetConversion)
                .Where(s => s != null)
                .Distinct();
        }

        public IEnumerable<StatementSyntax> GetDependencyInjectionRegistrations(IEnumerable<TypeConversion> conversions)
        {
            return conversions
                .Select(_autoconverterSyntaxService.GenerateServiceRegistrationStatementFromConversion)
                .Where(s => s != null);
        }

        public string GetFileName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StatementSyntax> GetGeneratorImplementation(IEnumerable<TypeConversion> conversions)
        {
            return conversions
                .Select(GetGeneratorImplementation)
                .Where(s => s != null);
        }

        private StatementSyntax GetGeneratorImplementation(TypeConversion conversion)
        {
            if (_conversionLogicService.ConverterExists(conversion.From, conversion.To))
            {
                return null;
            }

            _conversionLogicService.SetConverterAsExists(conversion.From, conversion.To);

            var fromProperties = _semanticAnalysisService.GetPropertySymbolsWithPublicGetter(conversion.From);
            var toProperties = _semanticAnalysisService.GetPropertySymbolsWithPublicSetter(conversion.To);

            return null;
        }
    }
}
