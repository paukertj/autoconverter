using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes
{
    internal sealed class SyntaxNodesRepository : ISyntaxNodesRepository
    {
        private IEnumerable<SyntaxNode> _nodes = Enumerable.Empty<SyntaxNode>();

        public IEnumerator<SyntaxNode> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            var nodes = syntaxNode
                .DescendantNodes()
                .OfType<SyntaxNode>()
                .ToList();

            _nodes = _nodes.Concat(nodes);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
