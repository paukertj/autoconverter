using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes;
using Paukertj.Autoconverter.Generator.Services.Builder;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Receivers.Proxy
{
	internal class ProxyReceiver : IProxyReceiver
	{
		private readonly List<ISyntaxReceiver> _receivers;
		private readonly IBuilderService _builderService;

        public ProxyReceiver(IBuilderService builderService)
		{
            _receivers = new List<ISyntaxReceiver>();
			_builderService = builderService;
        }

        public IProxyReceiver RegisterRepository<TSyntaxNode>()
			where TSyntaxNode : SyntaxNode
        {
			_builderService.AddSingletons<ISyntaxNodesRepository<TSyntaxNode>>();

			var repositories = _builderService.GetServices<ISyntaxNodesRepository<TSyntaxNode>>();

            _receivers.AddRange(repositories);

			return this;
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
