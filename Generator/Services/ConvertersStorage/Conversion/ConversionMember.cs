using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion
{
    public sealed class ConversionMember
    {
        public string FullName { get; }

        public string PureFullName { get; }

        public string PureFullNameNullable { get; }

        public IReadOnlyList<ConversionProperty> Properties { get; }

        public IReadOnlyList<string> Namespaces { get; }

        public bool CanBeNull { get; }

        public TypeKind TypeKind { get; }

        public ConversionMember(string fullName, string pureFullName, string pureFullNameNullable, IReadOnlyList<IPropertySymbol> properties, IReadOnlyList<string> namespaces, bool canBeNull, TypeKind typeKind)
        {
            FullName = fullName;
            PureFullName = pureFullName;
            PureFullNameNullable = pureFullNameNullable;
            Properties = properties
                .Select(p => new ConversionProperty(p))
                .ToList();
            Namespaces = namespaces;
            CanBeNull = canBeNull;
            TypeKind = typeKind;
        }
    }
}
