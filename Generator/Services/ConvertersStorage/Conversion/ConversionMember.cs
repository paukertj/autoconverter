using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion
{
    public sealed class ConversionMember
    {
        public string FullName { get; }

        public IReadOnlyList<ConversionProperty> Properties { get; }

        public IReadOnlyList<string> Namespaces { get; }

        public bool CanBeNull { get; }

        public ConversionMember(string fullName, IReadOnlyList<IPropertySymbol> properties, IReadOnlyList<string> namespaces, bool canBeNull)
        {
            FullName = fullName;
            Properties = properties
                .Select(p => new ConversionProperty(p))
                .ToList();
            Namespaces = namespaces;
            CanBeNull = canBeNull;
        }
    }
}
