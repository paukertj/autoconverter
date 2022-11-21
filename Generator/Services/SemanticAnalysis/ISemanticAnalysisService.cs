using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Paukertj.Autoconverter.Generator.Services.SemanticAnalysis
{
	public interface ISemanticAnalysisService
	{
		SemanticModel GetSemanticModel(SyntaxTree syntaxTree);

		bool MethodOf(GenericNameSyntax toAnalyze, string shoudBeMemberOfTypeName);

		IReadOnlyList<string> GetAllNamespaces(TypeSyntax syntaxNode);

		IReadOnlyList<string> GetAllNamespaces(ITypeSymbol typeSymbol);

		string GetNamespace(TypeSyntax syntaxNode);

		IReadOnlyList<IPropertySymbol> GetPropertySymbols(TypeSyntax typeSyntax);

		IReadOnlyList<IPropertySymbol> GetPropertySymbols(TypeInfo typeInfo);

		IReadOnlyList<IPropertySymbol> GetPropertySymbols(ImmutableArray<ISymbol>? symbols);
	}
}
