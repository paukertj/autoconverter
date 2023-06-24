using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes;
using Paukertj.Autoconverter.Generator.Services.ConversionLogic;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal sealed class ClassGeneratorPipe : IGeneratorConverter, IGeneratorDependencyInjectionRegistering
    {
        private readonly IConversionLogicService _conversionLogicService;
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly ISyntaxNodesRepository<GenericNameSyntax> _syntaxNodesRepository;
        private readonly List<StatementSyntax> _dependencyInjectionRegistrations;

        public ClassGeneratorPipe(
            IConversionLogicService conversionLogicService,
            ISemanticAnalysisService semanticAnalysisService,
            ISyntaxNodesRepository<GenericNameSyntax> syntaxNodesRepository)
        {
            _conversionLogicService = conversionLogicService;
            _semanticAnalysisService = semanticAnalysisService;
            _syntaxNodesRepository = syntaxNodesRepository;

            _dependencyInjectionRegistrations = new List<StatementSyntax>();
        }

        public IEnumerable<StatementSyntax> GetDependencyInjectionRegistrations()
        {
            return new List<StatementSyntax> ();
        }

        public string GetFileName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StatementSyntax> GetGeneratorImplementation()
        {
            return _syntaxNodesRepository
                .Where(n => n.Identifier.ValueText == nameof(IConvertingService.Convert))
                .Select(GetGeneratorImplementation)
                .Where(s => s != null);
        }

        private StatementSyntax GetGeneratorImplementation(GenericNameSyntax convertCallNode)
        {
            var genericsArguments = convertCallNode
                .DescendantNodes()
                .OfType<TypeArgumentListSyntax>()
                .FirstOrDefault()?.Arguments;

            if (genericsArguments == null || genericsArguments.Value.Count != 2)
            {
                return null;
            }

            var from = genericsArguments.Value.First();
            var to = genericsArguments.Value.Last();

            if (_conversionLogicService.ConverterExists(from, to))
            {
                return null;
            }

            _conversionLogicService.SetConverterAsExists(from, to);

            var fromProperties = _semanticAnalysisService.GetPropertySymbolsWithPublicGetter(from);
            var toProperties = _semanticAnalysisService.GetPropertySymbolsWithPublicSetter(to);

            return null;
        }
    }
}
