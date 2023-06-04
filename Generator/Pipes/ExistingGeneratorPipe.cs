using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage;
using Paukertj.Autoconverter.Generator.Services.Test;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal sealed class ExistingGeneratorPipe : ICodeGeneratingPipe, ICompilationPipe, ISyntaxReceiverPipe
    {
        public ITestService ConvertersStorageService { get; }

        public ExistingGeneratorPipe(ITestService convertersStorageService)
        {
            ConvertersStorageService = convertersStorageService;
        }

        public string GetFileName()
        {
            return string.Empty;
        }

        public string GetSourceCode()
        {
            return string.Empty;
        }

        public void OnCompilation(Compilation compilation)
        {

        }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            
        }
    }
}
