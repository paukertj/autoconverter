﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage
{
	internal class ConvertersStorageService : IConvertersStorageService
	{
		private readonly ISemanticAnalysisService _semanticAnalysisService;
		private readonly Dictionary<string, ConversionInfo> _conversionsStore = new Dictionary<string, ConversionInfo>();
		private readonly HashSet<string> _conversionsForm = new HashSet<string>();

		public ConvertersStorageService(ISemanticAnalysisService semanticAnalysisService)
		{
			_semanticAnalysisService = semanticAnalysisService;
		}

		public IReadOnlyList<ConversionInfo> GetConverters()
		{
			return _conversionsStore.Values
				.ToList();
		}

		public void StoreConverter(GenericNameSyntax genericNameSyntax)
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

		private void StoreMap(TypeSyntax fromTypeSyntax, TypeSyntax toTypeSyntax)
		{
			var from = GetConversionMember(fromTypeSyntax);
			var to = GetConversionMember(toTypeSyntax);

			StoreMap(from, to);
		}

		private void StoreMap(ConversionMember from, ConversionMember to)
		{
			ValidateForClassReferencesAndStore(from, to);

			var conversionInfo = new ConversionInfo(from, to);

			if (_conversionsStore.ContainsKey(conversionInfo.Id))
			{
				return;
			}

			// TODO: Validate map!

			_conversionsStore.Add(conversionInfo.Id, conversionInfo);
			_conversionsForm.Add(from.FullName);
		}

		private void ValidateForClassReferencesAndStore(ConversionMember from, ConversionMember to)
		{
			var fromPropertiesWithOfSomethingComplex = from.Properties
				.Where(HasTypeThatHasProperties)
				.ToList();

			if (fromPropertiesWithOfSomethingComplex.Any() == false)
			{
				return;
			}

			// So fromPropertiesWithOfSomethingComplex now contains list of properties with type, that need a new map

			CheckForOrphans(from, to);

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
			var orphans = from.Properties
				.GroupJoin(to.Properties, f => GetPropertyName(f), t => GetPropertyName(t), (f, t) => new { From = f, To = t })
				.Where(ft => ft.To?.Any() != true)
				.ToList();

			if (orphans.Count <= 0)
			{
				return;
			}

			// I want to show real name, so no GetPropertyName here!
			string unamppedMembersHumanReadable = string.Join(", ", orphans.Select(o => o.From.PropertySymbol.Name));

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
			return typeSymbol.SpecialType != SpecialType.None;
		}

		private string GetTypeFullName(TypeSyntax typeSyntax)
		{
			var ns = _semanticAnalysisService.GetNamespace(typeSyntax);
			var name = typeSyntax.ToString();

			return ns + '.' + name;
		}

		private ConversionMember GetConversionMember(TypeSyntax typeSyntax)
		{
			string fullName = GetTypeFullName(typeSyntax);

			var properties = _semanticAnalysisService.GetPropertySymbols(typeSyntax);
			var namespaces = _semanticAnalysisService.GetAllNamespaces(typeSyntax);

			return new ConversionMember(fullName, properties, namespaces);
		}

		private ConversionMember GetConversionMember(ITypeSymbol typeSymbol)
		{
			string fullName = typeSymbol.ToString();
			var members = typeSymbol.GetMembers();

			var properties = _semanticAnalysisService.GetPropertySymbols(members);
			var namespaces = _semanticAnalysisService.GetAllNamespaces(typeSymbol);

			return new ConversionMember(fullName, properties, namespaces);
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
