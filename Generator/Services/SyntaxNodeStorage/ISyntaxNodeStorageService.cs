using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage
{
	public interface ISyntaxNodeStorageService<TNode>
	{
		int Count();


		void Store(TNode node);

		void Store(IEnumerable<TNode> nodes);

		IReadOnlyList<TNode> Get();
	}
}
