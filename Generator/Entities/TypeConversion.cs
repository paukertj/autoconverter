using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Paukertj.Autoconverter.Generator.Entities
{
    public sealed class TypeConversion
    {
        public TypeSyntax From { get; }

        public TypeSyntax To { get; }

        public TypeConversion(TypeSyntax from, TypeSyntax to)
        {
            From = from;
            To = to;
        }
    }
}
