using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Paukertj.Autoconverter.Generator.Services.AutoconverterPropertyIgnore;
using System.Xml.Linq;
using System;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage
{
    internal class ConvertersStorageService : IConvertersStorageService
    {
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly IStaticAnalysisService _staticAnalysisService;
        private readonly Dictionary<string, ConversionInfoBase> _conversionsStore = new Dictionary<string, ConversionInfoBase>();
        private readonly IAutoconverterPropertyIgnoreService _autoconverterPropertyIgnoreService;

        public ConvertersStorageService(
            ISemanticAnalysisService semanticAnalysisService, 
            IStaticAnalysisService staticAnalysisService,
            IAutoconverterPropertyIgnoreService autoconverterPropertyIgnoreService)
        {
            _semanticAnalysisService = semanticAnalysisService;
            _staticAnalysisService = staticAnalysisService;
            _autoconverterPropertyIgnoreService = autoconverterPropertyIgnoreService;
        }

        public IReadOnlyList<TConverter> GetConverters<TConverter>()
            where TConverter : ConversionInfoBase
        {
            return _conversionsStore.Values
                .OfType<TConverter>()
                .ToList();
        }

        public void StoreGeneratedConverter(GenericNameSyntax genericNameSyntax)
        {
            var genericsArguments = genericNameSyntax
                .DescendantNodes()
                .OfType<TypeArgumentListSyntax>()
                .FirstOrDefault()?.Arguments;

            if (genericsArguments == null || genericsArguments.Value.Count != 2)
            {
                return;
            }

            StoreMap(genericsArguments.Value.First(), genericsArguments.Value.Last());
        }

        public void StoreExistingConverter(GenericNameSyntax genericNameSyntax)
        {
            var genericsArguments = genericNameSyntax
                .DescendantNodes()
                .OfType<TypeArgumentListSyntax>()
                .FirstOrDefault()?.Arguments;

            if (genericsArguments == null || genericsArguments.Value.Count != 2)
            {
                return;
            }

            var from = GetTypeFullName(genericsArguments.Value.First());
            var to = GetTypeFullName(genericsArguments.Value.Last());

            var implementation = _staticAnalysisService.GetClassOrRecord(genericNameSyntax);
            var implementationString = _staticAnalysisService.GetClassOrRecordNesteadName(implementation);
			var implementationNamespace = _staticAnalysisService.GetNamespace(implementation);
            var implementationNamespaceString = implementationNamespace.Name
                .ToFullString()
                .Trim();

            var conversionInfo = new ExistingConversionInfo(from, to, implementationNamespaceString, implementationString);

            _conversionsStore.Add(conversionInfo.Id, conversionInfo);
        }

        private void StoreMap(TypeSyntax fromTypeSyntax, TypeSyntax toTypeSyntax)
        {
            var from = GetConversionMember(fromTypeSyntax, _semanticAnalysisService.GetPropertySymbolsWithPublicGetter);
            var to = GetConversionMember(toTypeSyntax, _semanticAnalysisService.GetPropertySymbolsWithPublicSetter);

            StoreMap(from, to);
        }

        private void StoreMap(ConversionMember from, ConversionMember to)
        {
            var entryPoint = _staticAnalysisService.GetEntryPointInfo();
            
            var conversionInfo = new GeneratedConversionInfo(from, to, entryPoint.NamespaceName);

            if (_conversionsStore.ContainsKey(conversionInfo.Id))
            {
                return;
            }

            ValidateForClassReferencesAndStore(from, to); 

            _conversionsStore.Add(conversionInfo.Id, conversionInfo);
        }

        private void ValidateForClassReferencesAndStore(ConversionMember from, ConversionMember to)
        {
            CheckForOrphans(from, to);

            var fromPropertiesWithOfSomethingComplex = from.Properties
                .Where(HasTypeThatHasProperties)
                .ToList();

            if (fromPropertiesWithOfSomethingComplex.Any() == false)
            {
                return;
            }

            // So fromPropertiesWithOfSomethingComplex now contains list of properties with type, that need a new map

            var conversions = fromPropertiesWithOfSomethingComplex
                .Join(to.Properties, f => GetPropertyName(f), t => GetPropertyName(t), (f, t) => new ConversionTupple(f, t))
                .ToList();

            // I have to segregate generics - so for example if I have IEnumerable<T> I have to create convertor for T, the IEnumerable will be handled in converting service

            var conversionsToRemove = new List<ConversionTupple>();

            foreach (var conversion in conversions)
            {
                var fromConversionType = GetConversionTypeForGenerics(conversion.From);

                if (fromConversionType == null)
                {
                    conversionsToRemove.Add(conversion);
                    continue;
                }

                var toConversionType = GetConversionTypeForGenerics(conversion.To);

                if (toConversionType == null)
                {
                    conversionsToRemove.Add(conversion);
                    continue;
                }

                conversion.From.ChangeConversionType(fromConversionType);
                conversion.To.ChangeConversionType(toConversionType);

                // TODO: Here I have to say if it is list or not, if it is, I have to pass it in new instance (?)
            }

            conversions.RemoveRange(conversionsToRemove);

            foreach (var map in conversions)
            {
                var fromConversionMember = GetConversionMember(map.From.TypeSymbolForConversion);
                var toConversionMember = GetConversionMember(map.To.TypeSymbolForConversion);

                map.From.WillRequireConversion();
                map.To.WillRequireConversion();

                StoreMap(fromConversionMember, toConversionMember);
            }

            return;
        }

        private ITypeSymbol GetConversionTypeForGenerics(ConversionProperty conversionProperty)
        {
            if (conversionProperty.PropertySymbol.Type is not INamedTypeSymbol namedTypeSymbol)
            {
                return conversionProperty.TypeSymbolForConversion;
            }

            if (namedTypeSymbol.TypeArguments == null || namedTypeSymbol.TypeArguments.Length <= 0)
            {
                return conversionProperty.TypeSymbolForConversion; // No generics
            }

            var typeArgument = namedTypeSymbol.TypeArguments.Last(); // For multiple generics use last

            if (IsNotConvertible(typeArgument))
            {
                return null;
            }

            return typeArgument;
        }

        private void CheckForOrphans(ConversionMember from, ConversionMember to)
        {
            var mappedSources = from.Properties
                .Join(to.Properties, f => GetPropertyName(f), t => GetPropertyName(t), (f, t) => new { From = f, To = t })
                .ToList();

            if (mappedSources.Count == from.Properties.Count)
            {
                // I can map all source properties
                return;
            }

            var targetOrphans = to.Properties
                .Where(t => t.IgnoredForConverionToTypes.Contains(from.FullName) == false)
                .GroupJoin(from.Properties, t => GetPropertyName(t), f => GetPropertyName(f), (t, f) => new { To = t, From = f })
                .Where(ft => ft.From?.Any() != true)
                .ToList();

            if (targetOrphans.Count <= 0)
            {
                // I can map all target members (should never happen because condition above)
                return;
            }


            // I want to show real name, so no GetPropertyName here!
            string unamppedMembersHumanReadable = string.Join(", ", targetOrphans.Select(o => o.To.PropertySymbol.Name));

            throw new UnableToBuildMapException($"Unable to map '{from.FullName}' to '{to.FullName}'! There is no definition for these properties '{unamppedMembersHumanReadable}' from '{from.FullName}'!");
        }

        private string GetPropertyName(ConversionProperty conversionProperty)
        {
            // TODO: Some day i'll have to consider different names by some attribute here
            return conversionProperty.PropertySymbol.Name;
        }

        private bool HasTypeThatHasProperties(ConversionProperty conversionProperty)
        {
            if (IsNotConvertible(conversionProperty.TypeSymbolForConversion))
            {
                return false;
            }

            var members = conversionProperty.TypeSymbolForConversion.GetMembers();

            return _semanticAnalysisService
                .GetPropertySymbols(members)
                .Any();
        }

        private bool IsNotConvertible(ITypeSymbol typeSymbol)
        {
            // "False" means the we will have some primitive type in hands or something
            return typeSymbol.SpecialType != SpecialType.None || typeSymbol.BaseType?.ToDisplayString() == "System.ValueType";
        }

        private string GetTypeFullName(TypeSyntax typeSyntax)
        {
            var ns = _semanticAnalysisService.GetNamespace(typeSyntax);
            var name = typeSyntax.ToString();

            return ns + '.' + name;
        }

        private ConversionMember GetConversionMember(TypeSyntax typeSyntax, Func<TypeSyntax, IReadOnlyList<IPropertySymbol>> filter)
        {
            string fullName = GetTypeFullName(typeSyntax);

            var properties = filter(typeSyntax);
			var namespaces = _semanticAnalysisService.GetAllNamespaces(typeSyntax);
            
            var typeInfo = _semanticAnalysisService.GetTypeInfo(typeSyntax);
            var canBeNull = CanBeNull(typeInfo.Type);

            return GetConversionMember(fullName, properties, namespaces, canBeNull);
        }

        private ConversionMember GetConversionMember(ITypeSymbol typeSymbol)
        {
            string fullName = typeSymbol.ToString();
            var members = typeSymbol.GetMembers();

            var properties = _semanticAnalysisService.GetPropertySymbols(members);
            var namespaces = _semanticAnalysisService.GetAllNamespaces(typeSymbol);
            bool canBeNull = CanBeNull(typeSymbol);

            return GetConversionMember(fullName, properties, namespaces, canBeNull);
        }

        private bool CanBeNull(ITypeSymbol typeSymbol)
        {
            return typeSymbol.TypeKind != TypeKind.Struct;
        }

        private ConversionMember GetConversionMember(string fullName, IReadOnlyList<IPropertySymbol> properties, IReadOnlyList<string> namespaces, bool canBeNull)
        {
            var member = new ConversionMember(fullName, properties, namespaces, canBeNull);

            foreach (var property in member.Properties)
            {
                var ignoredForTypes = _autoconverterPropertyIgnoreService.IgnoredForTypes(property.PropertySymbol);
                property.IgnoreForConverionToTypes(ignoredForTypes.ToArray());
            }

            return member;
        }

        private sealed record ConversionTupple
        {
            public ConversionProperty From { get; }

            public ConversionProperty To { get; }

            public ConversionTupple(ConversionProperty from, ConversionProperty to)
            {
                From = from;
                To = to;
            }
        }
    }
}
