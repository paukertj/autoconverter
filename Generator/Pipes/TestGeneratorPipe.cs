using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Services.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal class TestGeneratorPipe : ISyntaxReceiverPipe
    {
        public ITestService ConvertersStorageService { get; }

        public TestGeneratorPipe(ITestService convertersStorageService)
        {
            ConvertersStorageService = convertersStorageService;
        }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            return;
        }
    }
}
