using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paukertj.Autoconverter.Generator.Tests.Exceptions
{
    public class TestCompilationRunException : Exception
    {
        public IEnumerable<Diagnostic> Diagnostic { get; }

        public TestCompilationRunException(IEnumerable<Diagnostic> diagnostic)
        {
            Diagnostic = diagnostic;
        }
    }
}
