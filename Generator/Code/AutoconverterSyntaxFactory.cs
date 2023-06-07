using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Paukertj.Autoconverter.Generator.Contexts;

namespace Paukertj.Autoconverter.Generator.Code
{
    internal static class AutoconverterSyntaxFactory
    {
        public const string ServiceCollectionParameter = "serviceCollection";
        public const string RegistrationLifetime = "AddSingleton";

        public static StatementSyntax GeneratorDepenedcyInjectionRegistration<TContext>(ConversionGeneratorContext<TContext> context)
            where TContext : TypeGeneratorContext
        {
            return ExpressionStatement(
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(ServiceCollectionParameter),
                        GenericName(
                            Identifier(RegistrationLifetime))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SeparatedList<TypeSyntax>(
                                    new SyntaxNodeOrToken[]{
                                                    GenericName(
                                                        Identifier("IConverter"))
                                                    .WithTypeArgumentList(
                                                        TypeArgumentList(
                                                            SeparatedList<TypeSyntax>(
                                                                new SyntaxNodeOrToken[]{
                                                                    IdentifierName(context.From.PureFullNameNullable),
                                                                    Token(SyntaxKind.CommaToken),
                                                                    IdentifierName(context.From.PureFullNameNullable)}))),
                                                    Token(SyntaxKind.CommaToken),
                                                    IdentifierName(context.GetGeneratorClassFullName())}))))));
        }

        public static ClassDeclarationSyntax GeneratorClass(string name, string from, string to)
        {
            return ClassDeclaration(name)
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithBaseList(
                        BaseList(
                            SingletonSeparatedList<BaseTypeSyntax>(
                                SimpleBaseType(
                                    GenericName(
                                        Identifier("IConverter"))
                                            .WithTypeArgumentList(
                                                TypeArgumentList(
                                                    SeparatedList<TypeSyntax>(
                                                        new SyntaxNodeOrToken[]
                                                        {
                                                            IdentifierName(from),
                                                            Token(SyntaxKind.CommaToken),
                                                            IdentifierName(to)
                                                        }
                                                    )
                                                )
                                            )
                                        )
                                )
                            )
                        );
        }
    }
}
