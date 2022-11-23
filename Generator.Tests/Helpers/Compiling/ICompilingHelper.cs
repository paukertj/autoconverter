using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Reflection;

namespace Paukertj.Autoconverter.Generator.Tests.Helpers.Compiling
{
    public interface ICompilingHelper
    {
        ICompilingHelper AddSourceCode(string filePath);

        ICompilingHelper AddAssemblyFromType<TType>();

        ICompilingHelper AddAssembly(Assembly assembly);

        ICompilingHelper Compile();

        IReadOnlyList<Diagnostic> GetDiagnosticOutput();

        Compilation GetCompilationOutput();
    }
}
