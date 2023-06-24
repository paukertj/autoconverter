using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Entities;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal interface IGeneratorDependencyInjectionRegistering : IAnalyzer
    {
        IEnumerable<StatementSyntax> GetDependencyInjectionRegistrations(IEnumerable<TypeConversion> conversions);
    }
}
