using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Paukertj.Autoconverter.Generator.Code
{
    internal static class CompilationUnitSyntaxExtensions
    {
        public static CompilationUnitSyntax WithNamespace<TNode>(this CompilationUnitSyntax compilationUnitSyntax, string name, SyntaxNode innerNode)
            where TNode : SyntaxNode
        {
            return compilationUnitSyntax
                .WithMembers(
                    SingletonList<MemberDeclarationSyntax>(
                        NamespaceDeclaration(
                            IdentifierName(name))
                                .WithMembers(
                                    SingletonList(innerNode)
                                )
                            )
                    );
        }
    }
}
