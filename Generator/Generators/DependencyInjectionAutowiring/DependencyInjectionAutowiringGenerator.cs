using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Linq;
using System;

namespace Paukertj.Autoconverter.Generator.Generators.DependencyInjectionAutowiring
{
    internal class DependencyInjectionAutowiringGenerator : IDependencyInjectionAutowiringGenerator
    {
        private const string ServiceCollectionParameter = "serviceCollection";
        private const string RegistrationLifetime = "AddSingleton";

        private readonly GeneratorExecutionContext _context;
        private readonly IConvertersStorageService _convertersStorageService;
        private readonly IStaticAnalysisService _staticAnalysisService;

        public DependencyInjectionAutowiringGenerator(
            GeneratorExecutionContext context,
            IConvertersStorageService convertersStorageService,
            IStaticAnalysisService staticAnalysisService)
        {
            _context = context;
            _convertersStorageService = convertersStorageService;
            _staticAnalysisService = staticAnalysisService;
        }

        public void AddAutowiring()
        {
            var converters = _convertersStorageService.GetConverters<ConversionInfoBase>();

			if (converters.Count <= 0)
			{
				return;
			}

            var converterServiceInfo = _staticAnalysisService.GetConverterServiceInfo();

            var serviceRegistrations = new List<StatementSyntax>(converters.Count);
            var usings = new List<string>
            {
                converterServiceInfo.NamespaceName,
                "Microsoft.Extensions.DependencyInjection"
            };

            foreach (var converter in converters)
            {
                var serviceRegistration = GetServiceRegistration(converter);
                serviceRegistrations.Add(serviceRegistration);

                var generatedConversionInfo = converter as GeneratedConversionInfo;

                if (generatedConversionInfo == null)
                {
                    continue;
                }

                usings.AddRange(generatedConversionInfo.From.Namespaces);
                usings.AddRange(generatedConversionInfo.To.Namespaces);
            }

            var entryPoint = _staticAnalysisService.GetEntryPointInfo();

            var usingDirectives = GetUsings(usings);

            var sourceCode = AddAutowiring(serviceRegistrations, entryPoint, converterServiceInfo, usingDirectives)
                    .NormalizeWhitespace()
                    .SyntaxTree
                    .GetText()
                    .ToString();

            _context.AddSource("DiCompositorAutomapping".GetFileName(), sourceCode);
        }

        private CompilationUnitSyntax AddAutowiring(List<StatementSyntax> serviceRegistrations, EntryPointInfo entryPointInfo, ConverterServiceInfo converterServiceInfo, List<UsingDirectiveSyntax> usings)
        {


            return CompilationUnit()
                .WithUsings(
                    List(usings.Distinct()))
                .WithMembers(
                    SingletonList<MemberDeclarationSyntax>(
                        NamespaceDeclaration(
                            IdentifierName(entryPointInfo.NamespaceName))
                        .WithMembers(
                            SingletonList<MemberDeclarationSyntax>(
                                ClassDeclaration(entryPointInfo.ClassName)
                                .WithModifiers(
                                    TokenList(
                                        new[]{
                                            Token(SyntaxKind.PublicKeyword),
                                            Token(SyntaxKind.StaticKeyword),
                                            Token(SyntaxKind.PartialKeyword)}))
                                .WithMembers(
                                    SingletonList<MemberDeclarationSyntax>(
                                        MethodDeclaration(
                                            PredefinedType(
                                                Token(SyntaxKind.VoidKeyword)),
                                            Identifier(entryPointInfo.MethodName))
                                        .WithModifiers(
                                            TokenList(
                                                new[]{
                                                    Token(SyntaxKind.StaticKeyword),
                                                    Token(SyntaxKind.PartialKeyword)}))
                                        .WithParameterList(
                                            ParameterList(
                                                SingletonSeparatedList(
                                                    Parameter(
                                                        Identifier(ServiceCollectionParameter))
                                                    .WithModifiers(
                                                        TokenList(
                                                            Token(SyntaxKind.ThisKeyword)))
                                                    .WithType(
                                                        IdentifierName("IServiceCollection")))))
                                        .WithBody(
                                            Block(serviceRegistrations))))))));
        }

        private List<UsingDirectiveSyntax> GetUsings(List<string> usings)
        {
            usings = usings
                .Distinct()
                .ToList();

            var usingsSyntax = new List<UsingDirectiveSyntax>(usings.Count);

            foreach (var usingValue in usings)
            {
                usingsSyntax.Add(
                    UsingDirective(
                            IdentifierName(usingValue)));
            }

            return usingsSyntax;
        }

        private ExpressionStatementSyntax GetServiceRegistration(ConversionInfoBase conversionInfo)
        {
            var generatedConversionInfo = conversionInfo as GeneratedConversionInfo;

            string fromFullName = generatedConversionInfo == null 
                ? conversionInfo.FromFullName 
                : generatedConversionInfo.From.PureFullNameNullable;
            string toFullName = generatedConversionInfo == null 
                ? conversionInfo.ToFullName 
                : generatedConversionInfo.To.PureFullNameNullable;

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
                                                            IdentifierName(fromFullName),
                                                            Token(SyntaxKind.CommaToken),
                                                            IdentifierName(toFullName)}))),
                                            Token(SyntaxKind.CommaToken),
                                            IdentifierName(conversionInfo.GetClassFullName())}))))));
        }
    }
}
