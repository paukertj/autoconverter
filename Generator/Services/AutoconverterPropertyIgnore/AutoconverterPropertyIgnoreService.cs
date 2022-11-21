using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.AutoconverterPropertyIgnore
{
    internal class AutoconverterPropertyIgnoreService : AttributeServiceBase, IAutoconverterPropertyIgnoreService
    {
        private readonly ISemanticAnalysisService _semanticAnalysisService;

        public AutoconverterPropertyIgnoreService(
            ISyntaxNodeStorageService<AttributeSyntax> syntaxNodeStorageService, 
            IStaticAnalysisService staticAnalysisService,
            ISemanticAnalysisService semanticAnalysisService) 
            : base(syntaxNodeStorageService, staticAnalysisService)
        {
            _semanticAnalysisService = semanticAnalysisService;
        }

        public IEnumerable<string> IgnoredForTypes(IPropertySymbol propertySymbol)
        {
            var attirbutes = GetAttirbutes(propertySymbol);

            if (attirbutes.Any() != true)
            {
                return new List<string>();
            }

            return attirbutes
                .SelectMany(a => a.ArgumentList.DescendantNodes())
                .OfType<IdentifierNameSyntax>()
                .Select(GetTypeFullName).ToList();
        }

        private string GetTypeFullName(IdentifierNameSyntax identifierNameSyntax)
        {
            // TODO: This will have weak performance...
            var model = _semanticAnalysisService.GetSemanticModel(identifierNameSyntax.SyntaxTree);
            return model.GetTypeInfo(identifierNameSyntax).Type.ToDisplayString();
        }
    }
}
