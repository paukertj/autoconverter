using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using System;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Receivers
{
	internal abstract class SyntaxNodeReceiverBase<TNode> : ISyntaxReceiver
		where TNode : SyntaxNode
	{
		private readonly ISyntaxNodeStorageService<TNode> _syntaxNodeStorageService;
		private readonly Func<TNode, bool> _identifier;

		protected SyntaxNodeReceiverBase(ISyntaxNodeStorageService<TNode> syntaxNodeStorageService, Func<TNode, bool> identifier)
		{
			_syntaxNodeStorageService = syntaxNodeStorageService;
			_identifier = identifier;
		}

		public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
		{
			var nodes = syntaxNode
				.DescendantNodes()
				.OfType<TNode>()
				.Where(_identifier)
				.ToList();

			_syntaxNodeStorageService.Store(nodes);
		}
	}
}
