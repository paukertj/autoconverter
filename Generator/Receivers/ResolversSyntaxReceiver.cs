using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using Paukertj.Autoconverter.Primitives.Resolvers;

namespace Paukertj.Autoconverter.Generator.Receivers
{
    internal class ResolversSyntaxReceiver : SyntaxNodeReceiverBase<MethodDeclarationSyntax>
    {
        public ResolversSyntaxReceiver(ISyntaxNodeStorageService<MethodDeclarationSyntax> syntaxNodeStorageService) 
            : base(syntaxNodeStorageService, GetIndetifier)
        {

        }

        private static bool GetIndetifier(MethodDeclarationSyntax interfaceDeclarationSyntax)
        {
            return interfaceDeclarationSyntax.Identifier.ValueText == nameof(Resolver.Resolve);
        }
    }
}
