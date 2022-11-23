using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paukertj.Autoconverter.Generator.Tests.Asserts
{
    public static class DiagnosticAsserts
    {
        public static void HasException<TException>(this Diagnostic diagnostic) 
            where TException : Exception, new()
        {
            var exception = new TException();

        }
    }
}
