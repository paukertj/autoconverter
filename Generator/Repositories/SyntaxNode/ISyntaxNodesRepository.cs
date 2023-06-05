using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes
{
    internal interface ISyntaxNodesRepository : IEnumerable<SyntaxNode>, ISyntaxReceiver
    {

    }
}
