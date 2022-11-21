using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage
{
	public sealed class ConversionMember
	{
		public string FullName { get; }

		public IReadOnlyList<ConversionProperty> Properties { get; }

		public IReadOnlyList<string> Namespaces { get; }

		public ConversionMember(string fullName, IReadOnlyList<IPropertySymbol> properties, IReadOnlyList<string> namespaces)
		{
			FullName = fullName;
			Properties = properties
				.Select(p => new ConversionProperty(p))
				.ToList();
			Namespaces = namespaces;
		}
	}
}
