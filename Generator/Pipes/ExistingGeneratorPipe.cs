using Microsoft.CodeAnalysis;
using System;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal sealed class ExistingGeneratorPipe : ICodeGeneratingPipe, ISemanticReceiverPipe, ISyntaxReceiverPipe
    {
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            
        }
    }
}
