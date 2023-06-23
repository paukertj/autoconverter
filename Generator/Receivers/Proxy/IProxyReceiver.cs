using Microsoft.CodeAnalysis;

namespace Paukertj.Autoconverter.Generator.Receivers.Proxy
{
	public interface IProxyReceiver : ISyntaxReceiver
	{
        IProxyReceiver RegisterRepository<TSyntaxNode>()
            where TSyntaxNode : SyntaxNode;
    }
}
