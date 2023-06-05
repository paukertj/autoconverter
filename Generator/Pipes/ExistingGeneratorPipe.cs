using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal sealed class ExistingGeneratorPipe : ICodeGeneratingPipe
    {
        private readonly IStaticAnalysisService _staticAnalysisService;
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly ISyntaxNodesRepository _syntaxNodesRepository;

        public ExistingGeneratorPipe(
            IStaticAnalysisService staticAnalysisService,
            ISemanticAnalysisService semanticAnalysisService,
            ISyntaxNodesRepository syntaxNodesRepository)
        {
            _staticAnalysisService = staticAnalysisService;
            _semanticAnalysisService = semanticAnalysisService;
            _syntaxNodesRepository = syntaxNodesRepository;
        }

        public string GetFileName()
        {
            return string.Empty;
        }

        public string GetSourceCode()
        {
            var nodes = FilterNodes();
            var conversions = OnCompilationInternal(nodes);

            return string.Empty;
        }

        private IEnumerable<GenericNameSyntax> FilterNodes()
        {
            return _syntaxNodesRepository
                .OfType<GenericNameSyntax>()
                .Where(p => p.Identifier.ValueText == nameof(IConverter<object, object>))
                .Distinct();
        }

        private IEnumerable<ExistingConversionInfo> OnCompilationInternal(IEnumerable<GenericNameSyntax> nodes)
        {
            foreach (var node in nodes)
            {
                var genericsArguments = node
                    .DescendantNodes()
                    .OfType<TypeArgumentListSyntax>()
                    .FirstOrDefault()?.Arguments;

                if (genericsArguments == null || genericsArguments.Value.Count != 2)
                {
                    continue;
                }

                var from = GetTypeFullName(genericsArguments.Value.First());
                var to = GetTypeFullName(genericsArguments.Value.Last());

                var implementation = _staticAnalysisService.GetClassOrRecord(node);
                var implementationString = _staticAnalysisService.GetClassOrRecordNesteadName(implementation);
                var implementationNamespace = _staticAnalysisService.GetNamespace(implementation);
                var implementationNamespaceString = implementationNamespace.Name
                    .ToFullString()
                    .Trim();

                yield return new ExistingConversionInfo(from, to, implementationNamespaceString, implementationString);
            }
        }

        private string GetTypeFullName(TypeSyntax typeSyntax)
        {
            return GetTypeFullName(typeSyntax, typeSyntax.ToString());
        }

        private string GetTypeFullName(TypeSyntax typeSyntax, string name)
        {
            var ns = _semanticAnalysisService.GetNamespace(typeSyntax);

            return ns + '.' + name;
        }
    }
}
