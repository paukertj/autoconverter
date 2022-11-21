using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Receivers.Proxy
{
	internal class ProxyReceiver : IProxyReceiver
	{
		private List<ISyntaxReceiver> _receivers = new List<ISyntaxReceiver>();

		public void RegisterSubscription(ISyntaxReceiver syntaxReceiver)
		{
			_receivers.Add(syntaxReceiver);
		}

		public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
		{
			foreach (var receiver in _receivers)
			{
				receiver.OnVisitSyntaxNode(syntaxNode);
			}
		}
	}
}
