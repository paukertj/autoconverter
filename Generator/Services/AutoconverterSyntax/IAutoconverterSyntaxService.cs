using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Entities;

namespace Paukertj.Autoconverter.Generator.Services.AutoconverterSyntax
{
    internal interface IAutoconverterSyntaxService
    {
        StatementSyntax GenerateServiceRegistrationStatementFromConversion(TypeConversion conversion);
    }
}
