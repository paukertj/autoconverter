using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes
{
    internal interface ISyntaxNodesRepository<TSyntaxNode> : IEnumerable<TSyntaxNode>, ISyntaxReceiver
        where TSyntaxNode : SyntaxNode
    {
        
    }
}
