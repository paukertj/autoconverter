using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.SemanticAnalysis
{
	public interface ISemanticAnalysisService
	{
		SemanticModel GetSemanticModel(SyntaxTree syntaxTree);

		TypeInfo GetTypeInfo(TypeSyntax typeSyntax);

        bool MemberOf<T>(GenericNameSyntax toAnalyze);

        IReadOnlyList<string> GetAllNamespaces(TypeSyntax syntaxNode);

		IReadOnlyList<string> GetAllNamespaces(ITypeSymbol typeSymbol);

		string GetNamespace(TypeSyntax syntaxNode);

		IReadOnlyList<IPropertySymbol> GetPropertySymbols(TypeSyntax typeSyntax);

		IReadOnlyList<IPropertySymbol> GetPropertySymbolsWithPublicSetter(TypeSyntax typeSyntax);

		IReadOnlyList<IPropertySymbol> GetPropertySymbolsWithPublicGetter(TypeSyntax typeSyntax);

		IReadOnlyList<IPropertySymbol> GetPropertySymbols(TypeInfo typeInfo);

		IReadOnlyList<IPropertySymbol> GetPropertySymbols(IReadOnlyList<ISymbol> symbols);
	}
}
