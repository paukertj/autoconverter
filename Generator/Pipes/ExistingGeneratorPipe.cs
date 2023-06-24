using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Code;
using Paukertj.Autoconverter.Generator.Contexts;
using Paukertj.Autoconverter.Generator.Entities;
using Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes;
using Paukertj.Autoconverter.Generator.Services.AutoconverterSyntax;
using Paukertj.Autoconverter.Generator.Services.ConversionLogic;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal sealed class ExistingGeneratorPipe : IGeneratorDependencyInjectionRegistering
    {
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly IAutoconverterSyntaxService _autoconverterSyntaxService;
        private readonly ISyntaxNodesRepository<GenericNameSyntax> _syntaxNodesRepository;

        public ExistingGeneratorPipe(
            ISemanticAnalysisService semanticAnalysisService,
            IAutoconverterSyntaxService autoconverterSyntaxService,
            ISyntaxNodesRepository<GenericNameSyntax> syntaxNodesRepository)
        {
            _semanticAnalysisService = semanticAnalysisService;
            _autoconverterSyntaxService = autoconverterSyntaxService;
            _syntaxNodesRepository = syntaxNodesRepository;
        }

        public IEnumerable<TypeConversion> GetData()
        {
            return _syntaxNodesRepository
                .ToList()
                .OfType<GenericNameSyntax>()
                .Where(p => p.Identifier.ValueText == nameof(IConverter<object, object>))
                .Select(_semanticAnalysisService.GetConversion)
                .Distinct();
        }

        public IEnumerable<StatementSyntax> GetDependencyInjectionRegistrations(IEnumerable<TypeConversion> conversions)
        {
            return conversions
                .Select(_autoconverterSyntaxService.GenerateServiceRegistrationStatementFromConversion)
                .Where(s => s != null);
        }
    }
}
