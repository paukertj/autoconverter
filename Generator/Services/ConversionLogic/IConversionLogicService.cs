using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paukertj.Autoconverter.Generator.Services.ConversionLogic
{
    internal interface IConversionLogicService
    {
        bool ConverterExists(TypeSyntax conversionFrom, TypeSyntax conversionTo);

        void SetConverterAsExists(TypeSyntax conversionFrom, TypeSyntax conversionTo);

        void ThrowIfCannotBeGenerated();
    }
}
