using Microsoft.CodeAnalysis;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal interface ICompilationPipe
    {
        void OnCompilation(Compilation compilation);
    }
}
