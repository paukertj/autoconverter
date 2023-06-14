using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Code;
using Paukertj.Autoconverter.Generator.Contexts;
using Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal sealed class ExistingGeneratorPipe : IGeneratorDependencyInjectionRegistering
    {
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly ISyntaxNodesRepository<GenericNameSyntax> _syntaxNodesRepository;

        public ExistingGeneratorPipe(
            ISemanticAnalysisService semanticAnalysisService,
            ISyntaxNodesRepository<GenericNameSyntax> syntaxNodesRepository)
        {
            _semanticAnalysisService = semanticAnalysisService;
            _syntaxNodesRepository = syntaxNodesRepository;
        }

        public IEnumerable<StatementSyntax> GetDependencyInjectionRegistrations()
        {
            return _syntaxNodesRepository
                .OfType<GenericNameSyntax>()
                .Where(p => p.Identifier.ValueText == nameof(IConverter<object, object>))
                .Distinct()
                .Select(GenerateStatementFromSyntax)
                .Where(s => s != null);
        }

        private StatementSyntax GenerateStatementFromSyntax(GenericNameSyntax syntax)
        {
            var genericsArguments = syntax
                .DescendantNodes()
                .OfType<TypeArgumentListSyntax>()
                .FirstOrDefault()?.Arguments;

            if (genericsArguments == null || genericsArguments.Value.Count != 2)
            {
                return null;
            }

            var from = genericsArguments.Value.First();
            var to = genericsArguments.Value.Last();

            var fromTypeGeneratorContext = _semanticAnalysisService.TypeSyntaxToTypeGeneratorContext(from);
            var toTypeGeneratorContext = _semanticAnalysisService.TypeSyntaxToTypeGeneratorContext(to);

            var typeGeneratorContext = new ConversionGeneratorContext<TypeGeneratorContext>(fromTypeGeneratorContext, toTypeGeneratorContext);

            return AutoconverterSyntaxFactory.GeneratorDepenedcyInjectionRegistration(typeGeneratorContext);
        }
    }
}
