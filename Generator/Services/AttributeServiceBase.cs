using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using System.Collections.Generic;
using Paukertj.Autoconverter.Generator.Extensions;

namespace Paukertj.Autoconverter.Generator.Services
{
    internal abstract class AttributeServiceBase
    {
        private Dictionary<string, List<AttributeSyntax>> _propertyCache = null;

        private readonly ISyntaxNodeStorageService<AttributeSyntax> _syntaxNodeStorageService;
        private readonly IStaticAnalysisService _staticAnalysisService;

        public AttributeServiceBase(
            ISyntaxNodeStorageService<AttributeSyntax> syntaxNodeStorageService,
            IStaticAnalysisService staticAnalysisService)
        {
            _syntaxNodeStorageService = syntaxNodeStorageService;
            _staticAnalysisService = staticAnalysisService;
        }

        protected IReadOnlyList<AttributeSyntax> GetAttirbutes(IPropertySymbol propertySymbol)
        {
            if (_propertyCache == null)
            {
                FillPropertyCache();
            }

            string propertyFullName = propertySymbol.ToDisplayString();

            if (_propertyCache.TryGetValue(propertyFullName, out var attributes))
            {
                return attributes;
            }

            return new List<AttributeSyntax>();
        }

        private void FillPropertyCache()
        {
            var attributes = _syntaxNodeStorageService.Get();
            _propertyCache = new Dictionary<string, List<AttributeSyntax>>();

            foreach (var attribute in attributes)
            {
                RegisterAttributeInPropertyCache(attribute);
            }
        }

        private void RegisterAttributeInPropertyCache(AttributeSyntax attributeSyntax)
        {
            string propertyFullName = GetAttributePropertyFullName(attributeSyntax);
            List<AttributeSyntax> attributes;

            if (_propertyCache.TryGetValue(propertyFullName, out attributes) == false)
            {
                attributes = new List<AttributeSyntax>();
                _propertyCache.Add(propertyFullName, attributes);
            }

            attributes.Add(attributeSyntax);
        }

        private string GetAttributePropertyFullName(AttributeSyntax attributeSyntax)
        {
            var attributeProperty = attributeSyntax.GetFirstParentNode<PropertyDeclarationSyntax>();
            var propertyClass = _staticAnalysisService.GetClassOrRecord(attributeProperty);
            var classNamespace = _staticAnalysisService.GetNamespace(propertyClass);

            return
                classNamespace.Name.ToFullString().Trim() + '.' +
                propertyClass.Identifier.ToFullString().Trim() + '.' +
                attributeProperty.Identifier.ToFullString().Trim();

            //var a = properties.Last().GetAttributes().First().ApplicationSyntaxReference.GetSyntax().DescendantNodes().OfType<GenericNameSyntax>().First();
        }
    }
}
