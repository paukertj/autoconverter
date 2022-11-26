using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Linq;
using System.Collections.Generic;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage;
using Paukertj.Autoconverter.Generator.Extensions;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Paukertj.Autoconverter.Generator.Generators.Converter
{
	internal class ConverterGenerator : IConverterGenerator
	{
		private const string FromParameter = "from";
		private const string ConvertigServiceName = "convertingService";

		private readonly IConvertersStorageService _convertersStorageService;
		private readonly GeneratorExecutionContext _context;
		private readonly IStaticAnalysisService _staticAnalysisService;

		public ConverterGenerator(
			GeneratorExecutionContext context,
			IConvertersStorageService convertersStorageService,
			IStaticAnalysisService staticAnalysisService)
		{
			_context = context;
			_convertersStorageService = convertersStorageService;
			_staticAnalysisService = staticAnalysisService;
		}

		public void AddGenerators()
		{
			var converters = _convertersStorageService.GetConverters();
			var entryPointInfo = _staticAnalysisService.GetEntryPointInfo();
			var convertingServiceInfo = _staticAnalysisService.GetConvertingServiceInfo();
			var converterServiceInfo = _staticAnalysisService.GetConverterServiceInfo();

			foreach (var converter in converters)
			{
				var sourceCode = AddGenerator(converter, entryPointInfo, convertingServiceInfo, converterServiceInfo)
					.NormalizeWhitespace()
					.SyntaxTree
					.GetText()
					.ToString();

				_context.AddSource(converter.GetFileName(), sourceCode);
			}
		}

		private CompilationUnitSyntax AddGenerator(ConversionInfo conversionInfo, EntryPointInfo entryPointInfo, ConvertingServiceInfo convertingServiceInfo, ConverterServiceInfo converterServiceInfo)
		{
			return CompilationUnit()
				.WithUsings(List(GetUsings(conversionInfo, convertingServiceInfo, converterServiceInfo)))
				.WithMembers(
					SingletonList<MemberDeclarationSyntax>(
						NamespaceDeclaration(
							IdentifierName(entryPointInfo.NamespaceName))
						.WithMembers(
							SingletonList(GetConverterClass(conversionInfo, convertingServiceInfo)))));
		}

		private MemberDeclarationSyntax[] GetConvertingService(ConversionInfo conversionInfo, ConvertingServiceInfo convertingServiceInfo)
		{
			return new MemberDeclarationSyntax[]
			{
					FieldDeclaration(
						VariableDeclaration(
							IdentifierName(convertingServiceInfo.InterfaceName))
						.WithVariables(
							SingletonSeparatedList(
								VariableDeclarator(
									Identifier(CreateField(ConvertigServiceName))))))
					.WithModifiers(
						TokenList(
							new []{
								Token(SyntaxKind.PrivateKeyword),
								Token(SyntaxKind.ReadOnlyKeyword)})),
					ConstructorDeclaration(
						Identifier(conversionInfo.GetClassName()))
					.WithModifiers(
						TokenList(
							Token(SyntaxKind.PublicKeyword)))
					.WithParameterList(
						ParameterList(
							SingletonSeparatedList(
								Parameter(
									Identifier(ConvertigServiceName))
								.WithType(
									IdentifierName(convertingServiceInfo.InterfaceName)))))
					.WithBody(
						Block(
							SingletonList<StatementSyntax>(
								ExpressionStatement(
									AssignmentExpression(
										SyntaxKind.SimpleAssignmentExpression,
										IdentifierName(CreateField(ConvertigServiceName)),
										IdentifierName(ConvertigServiceName))))))
			};
		}

		private UsingDirectiveSyntax[] GetUsings(ConversionInfo conversionInfo, ConvertingServiceInfo convertingServiceInfo, ConverterServiceInfo converterServiceInfo)
		{
			var allUsings = new List<string>();

			allUsings.AddRange(conversionInfo.From.Namespaces);
			allUsings.AddRange(conversionInfo.To.Namespaces);
			allUsings.Add(converterServiceInfo.NamespaceName);
			allUsings.Add(convertingServiceInfo.NamespaceName);
			allUsings.Add(typeof(ArgumentNullException).Namespace);

			allUsings = allUsings
				.Distinct()
				.ToList();

			var usingsSyntax = new List<UsingDirectiveSyntax>(allUsings.Count);

			foreach (var usingValue in allUsings)
			{
				usingsSyntax.Add(
					UsingDirective(
							IdentifierName(usingValue)));
			}

			return usingsSyntax.ToArray();
		}

		private MemberDeclarationSyntax GetConverterClass(ConversionInfo conversionInfo, ConvertingServiceInfo convertingServiceInfo)
		{
			var classBody = new List<MemberDeclarationSyntax>();

			if (conversionInfo.RequireConversion)
			{
				classBody.AddRange(GetConvertingService(conversionInfo, convertingServiceInfo));
			}

			classBody.Add(
				MethodDeclaration(
					IdentifierName(conversionInfo.To.FullName),
					Identifier("Convert"))
				.WithModifiers(
					TokenList(
						Token(SyntaxKind.PublicKeyword)))
				.WithParameterList(
					ParameterList(
						SingletonSeparatedList(
							Parameter(
								Identifier(
									TriviaList(),
									SyntaxKind.FromKeyword,
									FromParameter,
									FromParameter,
									TriviaList()))
							.WithType(
								IdentifierName(conversionInfo.From.FullName)))))
				.WithBody(
					Block(
						GetNullCheck(),
						GetConversion(conversionInfo))));

			return ClassDeclaration(conversionInfo.GetClassName())
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
												new SyntaxNodeOrToken[]{
													IdentifierName(conversionInfo.From.FullName),
													Token(SyntaxKind.CommaToken),
													IdentifierName(conversionInfo.To.FullName)})))))))
					.WithMembers(List(classBody));
		}

		private IfStatementSyntax GetNullCheck()
		{
			return IfStatement(
				BinaryExpression(
					SyntaxKind.EqualsExpression,
					IdentifierName(
						Identifier(
							TriviaList(),
							SyntaxKind.FromKeyword,
							FromParameter,
							FromParameter,
							TriviaList())),
					LiteralExpression(
						SyntaxKind.DefaultLiteralExpression,
						Token(SyntaxKind.DefaultKeyword))),
				Block(
					SingletonList<StatementSyntax>(
						ReturnStatement(
							LiteralExpression(
								SyntaxKind.DefaultLiteralExpression,
								Token(SyntaxKind.DefaultKeyword))))));
		}

		private ReturnStatementSyntax GetConversion(ConversionInfo conversionInfo)
		{
			var properties = conversionInfo.From.Properties
                .Join(conversionInfo.To.Properties
                    .Where(t => t.IgnoredForConverionToTypes.Contains(conversionInfo.From.FullName) == false), from => from.PropertySymbol.Name, to => to.PropertySymbol.Name, (from, to) => GetPropertyConversion(from, to))
				.ToList();

			int size = properties.Count * 2 - 1;
			var conversionMap = new SyntaxNodeOrToken[size < 0 ? 0 : size];

			int x = 0;
			for (int y = 0; y < conversionMap.Length; y++)
			{
				if ((y + 1) % 2 == 0)
				{
					conversionMap[y] = Token(SyntaxKind.CommaToken);
				}
				else
				{
					conversionMap[y] = properties[x++];
				}
			}

			var conversion = ReturnStatement(
					ObjectCreationExpression(
						IdentifierName(conversionInfo.To.FullName))
					.WithInitializer(
						InitializerExpression(
							SyntaxKind.ObjectInitializerExpression,
							SeparatedList<ExpressionSyntax>(conversionMap))));

			return conversion;
		}

		private SyntaxNodeOrToken GetPropertyConversion(ConversionProperty from, ConversionProperty to)
		{
			bool requireConversion = from.RequireConversion && to.RequireConversion;

			if (requireConversion)
			{
				return AssignmentExpression(
					SyntaxKind.SimpleAssignmentExpression,
					IdentifierName(to.PropertySymbol.Name),
					InvocationExpression(
						MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							IdentifierName(CreateField(ConvertigServiceName)),
							GenericName(
								Identifier("Convert"))
							.WithTypeArgumentList(
								TypeArgumentList(
									SeparatedList<TypeSyntax>(
										new SyntaxNodeOrToken[]{
											IdentifierName(from.GetTypeFullName()),
											Token(SyntaxKind.CommaToken),
											IdentifierName(to.GetTypeFullName())})))))
					.WithArgumentList(
						ArgumentList(
							SingletonSeparatedList(
								Argument(
									MemberAccessExpression(
										SyntaxKind.SimpleMemberAccessExpression,
										IdentifierName(
											Identifier(
												TriviaList(),
												SyntaxKind.FromKeyword,
												FromParameter,
												FromParameter,
												TriviaList())),
										IdentifierName(from.PropertySymbol.Name)))))));
			}

			return AssignmentExpression(
					SyntaxKind.SimpleAssignmentExpression,
					IdentifierName(to.PropertySymbol.Name),
					MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						IdentifierName(
							Identifier(
								TriviaList(),
								SyntaxKind.FromKeyword,
								FromParameter,
								FromParameter,
								TriviaList())),
						IdentifierName(from.PropertySymbol.Name)));
		}

		private string CreateField(string variable)
		{
			return '_' + variable;
		}
	}
}
