using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes
{
    internal sealed class SyntaxNodesRepository<TSyntaxNode> : ISyntaxNodesRepository<TSyntaxNode>
        where TSyntaxNode : SyntaxNode
    {
        private IEnumerable<TSyntaxNode> _nodes = Enumerable.Empty<TSyntaxNode>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            var nodes = syntaxNode
                .DescendantNodes()
                .OfType<TSyntaxNode>()
                .ToList();

            _nodes = _nodes.Concat(nodes);
        }

        public IEnumerator<TSyntaxNode> GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
