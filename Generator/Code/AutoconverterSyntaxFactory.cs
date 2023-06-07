using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Contexts;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using System.Collections;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Code
{
    internal static class AutoconverterSyntaxFactory
    {
        public const string ServiceCollectionParameter = "serviceCollection";
        public const string RegistrationLifetime = "AddSingleton";
        public const string ConverterBaseIface = "IConverter";
        public const string ServiceCollectionIface = "IServiceCollection";

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
                                    new SyntaxNodeOrToken[]
                                    {
                                        GenericName(
                                            Identifier(ConverterBaseIface))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SeparatedList<TypeSyntax>(
                                                            new SyntaxNodeOrToken[]
                                                            {
                                                                IdentifierName(context.From.PureFullNameNullable),
                                                                Token(SyntaxKind.CommaToken),
                                                                IdentifierName(context.From.PureFullNameNullable)
                                                            })
                                                        )
                                                    ),
                                                    Token(SyntaxKind.CommaToken),
                                                    IdentifierName(context.GetGeneratorClassFullName()
                                        )
                                    }
                                )
                            )
                        )
                    )
                )
            );
        }

        public static ClassDeclarationSyntax DependencyInjectionRegistrationClass(EntryPointInfo entryPointInfo, MemberDeclarationSyntax innerMethod)
        {
            return
                ClassDeclaration(entryPointInfo.ClassName)
                    .WithModifiers(
                        TokenList(
                            new[]
                            {
                                Token(SyntaxKind.PublicKeyword),
                                Token(SyntaxKind.StaticKeyword),
                                Token(SyntaxKind.PartialKeyword)
                            }
                        )
                    )
                    .WithMembers(
                        SingletonList(innerMethod)
                    );
        }

        public static MethodDeclarationSyntax DependencyInjectionRegistrationExtension(EntryPointInfo entryPointInfo, IEnumerable<StatementSyntax> registrations)
        {
            return
                MethodDeclaration(
                    PredefinedType(
                        Token(SyntaxKind.VoidKeyword)
                    ),
                    Identifier(entryPointInfo.MethodName))
                        .WithModifiers(
                            TokenList(
                                new[]
                                {
                                    Token(SyntaxKind.StaticKeyword),
                                    Token(SyntaxKind.PartialKeyword)
                                }
                            )
                        )
                        .WithParameterList(
                            ParameterList(
                                SingletonSeparatedList(
                                    Parameter(
                                        Identifier(ServiceCollectionParameter)
                                    )
                                    .WithModifiers(
                                        TokenList(
                                            Token(SyntaxKind.ThisKeyword)
                                        )
                                    )
                                    .WithType(
                                        IdentifierName(ServiceCollectionIface)
                                    )
                                )
                            )
                        )
                        .WithBody(
                            Block(registrations)
                        );
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
                                        Identifier(ConverterBaseIface))
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
