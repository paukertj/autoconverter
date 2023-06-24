using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Entities;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal interface IGeneratorConverter : IAnalyzer
    {
        IEnumerable<StatementSyntax> GetGeneratorImplementation(IEnumerable<TypeConversion> conversions);
    }
}
