using Microsoft.CodeAnalysis;

namespace Paukertj.Autoconverter.Generator.Extensions
{
	internal static class SyntaxNodeExtensions
	{
		internal static TNode GetFirstParentNode<TNode>(this SyntaxNode node)
			where TNode : SyntaxNode
		{
			if (node == null)
			{
				return null;
			}

			if (node is TNode targetNode)
			{
				return targetNode;
			}

			return node.Parent.GetFirstParentNode<TNode>();
		}
	}
}
