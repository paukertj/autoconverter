using Microsoft.CodeAnalysis;

namespace Paukertj.Autoconverter.Generator.Receivers.Proxy
{
	public interface IProxyReceiver : ISyntaxReceiver
	{
		void RegisterSubscription(ISyntaxReceiver syntaxReceiver);
	}
}
