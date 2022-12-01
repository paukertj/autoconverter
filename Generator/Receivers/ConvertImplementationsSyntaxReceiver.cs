using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using Paukertj.Autoconverter.Primitives.Services.Converter;

namespace Paukertj.Autoconverter.Generator.Receivers
{
    internal class ConvertImplementationsSyntaxReceiver : SyntaxNodeReceiverBase<GenericNameSyntax>
    {
        public ConvertImplementationsSyntaxReceiver(ISyntaxNodeStorageService<GenericNameSyntax> syntaxNodeStorageService)
            : base(syntaxNodeStorageService, GetIndetifier)
        {

        }

        private static bool GetIndetifier(GenericNameSyntax interfaceDeclarationSyntax)
        {
            return interfaceDeclarationSyntax.Identifier.ValueText == nameof(IConverter<object,object>);
        }
    }
}
