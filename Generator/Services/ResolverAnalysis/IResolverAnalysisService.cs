using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Paukertj.Autoconverter.Generator.Services.ResolverAnalysis
{
    public interface IResolverAnalysisService
    {
        void StoreResolver(MethodDeclarationSyntax methodDeclarationSyntax);
    }
}
