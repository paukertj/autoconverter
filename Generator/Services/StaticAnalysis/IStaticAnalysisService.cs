using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Paukertj.Autoconverter.Generator.Services.StaticAnalysis
{
	internal interface IStaticAnalysisService
	{
		EntryPointInfo GetEntryPointInfo();

		ConvertingServiceInfo GetConvertingServiceInfo();

		ConverterServiceInfo GetConverterServiceInfo();

		TypeDeclarationSyntax GetClassOrRecord(SyntaxNode entrySyntaxNode);

		BaseNamespaceDeclarationSyntax GetNamespace(SyntaxNode entrySyntaxNode);
    }
}
