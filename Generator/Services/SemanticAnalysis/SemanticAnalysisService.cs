using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.SemanticAnalysis
{
	internal class SemanticAnalysisService : ISemanticAnalysisService
	{
		private SemanticModel _semanticModel = null;

		private readonly GeneratorExecutionContext _context;

		public SemanticAnalysisService(GeneratorExecutionContext context)
		{
			_context = context;
		}

		public SemanticModel GetSemanticModel(SyntaxTree syntaxTree)
		{
			SetSemanticModel(syntaxTree);

			return _semanticModel;
		}

		public bool MethodOf(GenericNameSyntax toAnalyze, string shoudBeMemberOfTypeName)
		{
			if (toAnalyze == null || string.IsNullOrWhiteSpace(shoudBeMemberOfTypeName))
			{
				return false;
			}

			var firstParentNode = toAnalyze.Parent?
				.DescendantNodes()?
				.FirstOrDefault();

			if (firstParentNode is null)
			{
				return false;
			}

			return MethodOf(toAnalyze, firstParentNode, shoudBeMemberOfTypeName);
		}

		public IReadOnlyList<string> GetAllNamespaces(TypeSyntax syntaxNode)
		{
			var result = new HashSet<string>();

			if (syntaxNode == null)
			{
				return new List<string>();
			}

			var semanticModel = GetSemanticModel(syntaxNode.SyntaxTree);

			var typeInfo = semanticModel.GetTypeInfo(syntaxNode);

			string classNamespace = GetNamespace(typeInfo);

			if (string.IsNullOrEmpty(classNamespace) == false)
			{
				result.Add(classNamespace);
			}

			var properties = GetPropertySymbols(typeInfo);

			EnrichNamespaces(properties, result);

			return result.ToList();
		}

		public IReadOnlyList<string> GetAllNamespaces(ITypeSymbol typeSymbol)
		{
			var result = new HashSet<string>();

			string classNamespace = typeSymbol.ContainingNamespace.ToDisplayString();

			if (string.IsNullOrEmpty(classNamespace) == false)
			{
				result.Add(classNamespace);
			}

			var members = typeSymbol.GetMembers();

			var properties = GetPropertySymbols(members);

			EnrichNamespaces(properties, result);

			return result.ToList();
		}

		private void EnrichNamespaces(IReadOnlyList<IPropertySymbol> properties, HashSet<string> result)
		{
			if (properties == null)
			{
				return;
			}

			foreach (var iface in properties)
			{
				string ifaceNamespace = iface?.ContainingNamespace?.ToDisplayString();

				if (string.IsNullOrEmpty(ifaceNamespace))
				{
					continue;
				}

				result.Add(ifaceNamespace);
			}
		}

		public string GetNamespace(TypeSyntax syntaxNode)
		{
			if (syntaxNode == null)
			{
				return null;
			}

			var semanticModel = GetSemanticModel(syntaxNode.SyntaxTree);

			var typeInfo = semanticModel.GetTypeInfo(syntaxNode);

			return GetNamespace(typeInfo);
		}

		private string GetNamespace(TypeInfo typeInfo)
		{
			return typeInfo
				.Type?
				.ContainingNamespace?
				.ToDisplayString();
		}

		public IReadOnlyList<IPropertySymbol> GetPropertySymbols(TypeSyntax typeSyntax)
		{
			var semanticModel = GetSemanticModel(typeSyntax.SyntaxTree);

			var typeInfo = semanticModel.GetTypeInfo(typeSyntax);

			if (typeInfo.Type == null)
			{
				return new List<IPropertySymbol>();
			}

			return GetPropertySymbols(typeInfo);
		}

		public IReadOnlyList<IPropertySymbol> GetPropertySymbols(TypeInfo typeInfo)
		{
			var members = typeInfo.Type?.GetMembers();

			return GetPropertySymbols(members);
		}

		public IReadOnlyList<IPropertySymbol> GetPropertySymbols(ImmutableArray<ISymbol>? symbols)
		{
			if (symbols == null)
			{
				return new List<IPropertySymbol>();
			}

			return symbols
				.OfType<IPropertySymbol>()
				.Where(p => p.IsImplicitlyDeclared == false)
				.ToList();
		}

		private bool MethodOf(GenericNameSyntax toAnalyze, SyntaxNode syntaxNode, string shoudBeMemberOfTypeName)
		{
			var semanticModel = GetSemanticModel(toAnalyze.SyntaxTree);

			var declarationTypeInfo = semanticModel.GetTypeInfo(syntaxNode);

			return declarationTypeInfo.Type?.Name == shoudBeMemberOfTypeName;
		}

		private void SetSemanticModel(SyntaxTree syntaxTree)
		{
			_semanticModel = _context.Compilation.GetSemanticModel(syntaxTree);
		}
	}
}
