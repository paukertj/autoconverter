using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal interface IGeneratorConverter
    {
        string GetFileName();

        IEnumerable<StatementSyntax> GetGeneratorImplementation();
    }
}
