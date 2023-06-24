using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.ConversionLogic
{
    internal sealed class ConversionLogicService : IConversionLogicService
    {
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly HashSet<string> _conversions;

        public ConversionLogicService(ISemanticAnalysisService semanticAnalysisService)
        {
            _semanticAnalysisService = semanticAnalysisService;
            _conversions = new HashSet<string>();
        }


        public bool ConverterExists(TypeSyntax conversionFrom, TypeSyntax conversionTo)
        {
            string key = GetKey(conversionFrom, conversionTo);

            return _conversions.Contains(key);
        }

        public void SetConverterAsExists(TypeSyntax conversionFrom, TypeSyntax conversionTo)
        {
            string key = GetKey(conversionFrom, conversionTo);

            _conversions.Add(key);
        }

        public void ThrowIfCannotBeGenerated()
        {
            throw new System.NotImplementedException();
        }

        private string GetKey(TypeSyntax conversionFrom, TypeSyntax conversionTo)
        {
            string conversionFromFullName = _semanticAnalysisService.GetTypePureFullName(conversionFrom);
            string conversionToFullName = _semanticAnalysisService.GetTypePureFullName(conversionTo);

            return conversionFromFullName + "->" + conversionToFullName;
        }
    }
}
