using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage
{
	internal class SyntaxNodeStorageService<TNode> : ISyntaxNodeStorageService<TNode>
	{
		private readonly HashSet<TNode> _nodes = new HashSet<TNode>();

		public int Count()
		{
			return _nodes.Count;
		}

		public IReadOnlyList<TNode> Get()
		{
			return _nodes.ToList();
		}

		public void Store(TNode node)
		{
			if (node == null)
			{
				return;
			}

			_nodes.Add(node);
		}

		public void Store(IEnumerable<TNode> nodes)
		{
			if (nodes == null)
			{
				return;
			}

			foreach (var node in nodes)
			{
				Store(node);
			}
		}
	}
}
