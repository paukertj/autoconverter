using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Generator.Extensions;

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
			var converters = _convertersStorageService.GetConverters();

			var serviceRegistrations = new List<StatementSyntax>(converters.Count);

			foreach (var converter in converters)
			{
				var serviceRegistration = GetServiceRegistration(converter);
				serviceRegistrations.Add(serviceRegistration);
			}

			var entryPoint = _staticAnalysisService.GetEntryPointInfo();
			var converterServiceInfo = _staticAnalysisService.GetConverterServiceInfo();

			var sourceCode = AddAutowiring(serviceRegistrations, entryPoint, converterServiceInfo)
					.NormalizeWhitespace()
					.SyntaxTree
					.GetText()
					.ToString();

			_context.AddSource("DiCompositorAutomapping".GetFileName(), sourceCode);
		}

		private CompilationUnitSyntax AddAutowiring(List<StatementSyntax> serviceRegistrations, EntryPointInfo entryPointInfo, ConverterServiceInfo converterServiceInfo)
		{
			return CompilationUnit()
				.WithUsings(
					List(
						new UsingDirectiveSyntax[]{
							UsingDirective(IdentifierName(converterServiceInfo.NamespaceName)),
							UsingDirective(IdentifierName("Microsoft.Extensions.DependencyInjection"))}))
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

		private ExpressionStatementSyntax GetServiceRegistration(ConversionInfo conversionInfo)
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
															IdentifierName(conversionInfo.From.FullName),
															Token(SyntaxKind.CommaToken),
															IdentifierName(conversionInfo.To.FullName)}))),
											Token(SyntaxKind.CommaToken),
											IdentifierName(conversionInfo.GetClassName())}))))));
		}
	}
}
