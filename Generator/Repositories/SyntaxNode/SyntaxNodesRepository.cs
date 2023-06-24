using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes
{
    internal sealed class SyntaxNodesRepository<TSyntaxNode> : ISyntaxNodesRepository<TSyntaxNode>
        where TSyntaxNode : SyntaxNode
    {
        private List<TSyntaxNode> _nodes = new List<TSyntaxNode>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TSyntaxNode t)
            {
                _nodes.Add(t);
            }
        }

        public IEnumerator<TSyntaxNode> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }
    }
}
