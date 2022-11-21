using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using Paukertj.Autoconverter.Generator.Exceptions;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.StaticAnalysis
{
	internal class StaticAnalysisService : IStaticAnalysisService
	{
		private EntryPointInfo _entryPointInfo;
		private ConvertingServiceInfo _convertingServiceInfo;
		private ConverterServiceInfo _converterServiceInfo;

		private readonly ISyntaxNodeStorageService<GenericNameSyntax> _convertMethodCalls;
		private readonly ISyntaxNodeStorageService<AttributeSyntax> _entryPointAttribtues;

		public StaticAnalysisService(
			ISyntaxNodeStorageService<GenericNameSyntax> convertMethodCalls,
			ISyntaxNodeStorageService<AttributeSyntax> entryPointAttribtues)
		{
			_convertMethodCalls = convertMethodCalls;
			_entryPointAttribtues = entryPointAttribtues;
		}

		public EntryPointInfo GetEntryPointInfo()
		{
			if (_entryPointInfo != null)
			{
				return _entryPointInfo;
			}

			var entryPoints = _entryPointAttribtues.Get();

			if (entryPoints.Count <= 0)
			{
				throw new ThereIsNoEntryPointException("Unable to find entry point method!");
			}

			if (entryPoints.Count > 1)
			{
				throw new ThereAreMoreThanOneEntryPointException($"There are '{entryPoints.Count}' entry points but only one expected!");
			}

			var entryPoint = entryPoints.First();

			var entryPointMethod = entryPoint.GetFirstParentNode<MethodDeclarationSyntax>();

			if (entryPointMethod == null)
			{
				throw new InvalidEntryPointException($"Can not get '{nameof(MethodDeclarationSyntax)}' from declared entry point!");
			}

			var entryPointClass = entryPoint.GetFirstParentNode<ClassDeclarationSyntax>();

			if (entryPointClass == null)
			{
				throw new InvalidEntryPointException($"Can not get '{nameof(ClassDeclarationSyntax)}' from declared entry point!");
			}

			var entryPointNamespace = entryPointClass.GetFirstParentNode<NamespaceDeclarationSyntax>();

			if (entryPointNamespace == null)
			{
				throw new InvalidEntryPointException($"Can not get '{nameof(NamespaceDeclarationSyntax)}' from declared entry point!");
			}

			_entryPointInfo = new EntryPointInfo(
				entryPointMethod.Identifier.ValueText
					.Trim(),
				entryPointClass.Identifier.ValueText
					.Trim(),
				entryPointNamespace.Name
					.ToFullString()
					.Trim());

			return _entryPointInfo;
		}

		public ConvertingServiceInfo GetConvertingServiceInfo()
		{
			if (_convertingServiceInfo != null)
			{
				return _convertingServiceInfo;
			}

			_convertingServiceInfo = new ConvertingServiceInfo(nameof(IConvertingService), typeof(IConvertingService).Namespace);

			return _convertingServiceInfo;
		}

		public ConverterServiceInfo GetConverterServiceInfo()
		{
			if (_converterServiceInfo != null)
			{
				return _converterServiceInfo;
			}

			_converterServiceInfo = new ConverterServiceInfo(nameof(IConverter<object, object>), typeof(IConverter<object, object>).Namespace);

			return _converterServiceInfo;
		}
	}
}
