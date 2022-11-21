using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Exceptions;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Generators.Converter;
using Paukertj.Autoconverter.Generator.Generators.DependencyInjectionAutowiring;
using Paukertj.Autoconverter.Generator.Receivers;
using Paukertj.Autoconverter.Generator.Receivers.Proxy;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using System;
using System.Diagnostics;

namespace Paukertj.Autoconverter.Generator
{
	[Generator]
	internal class AutomappingGenerator : ISourceGenerator
	{
		private readonly ISyntaxNodeStorageService<GenericNameSyntax> _convertMethodCalls;
		private readonly ISyntaxNodeStorageService<AttributeSyntax> _entryPointAttribtues;
		private readonly IStaticAnalysisService _staticAnalysisService;

		public AutomappingGenerator()
		{
			_convertMethodCalls = new SyntaxNodeStorageService<GenericNameSyntax>();
			_entryPointAttribtues = new SyntaxNodeStorageService<AttributeSyntax>();

			_staticAnalysisService = new StaticAnalysisService(_convertMethodCalls, _entryPointAttribtues);
		}

		public void Execute(GeneratorExecutionContext context)
		{
			try
			{
				ExecuteInternal(context);
			}
			catch (AutmappingExceptionBase e)
			{
				context.ReportDiagnostic(e);
			}
			catch (Exception e)
			{
				context.ReportDiagnostic(e);
			}
		}

		private void ExecuteInternal(GeneratorExecutionContext context)
		{
			var semanticAnalysisService = new SemanticAnalysisService(context);
			var convertersStorageService = new ConvertersStorageService(semanticAnalysisService);
			var converterGenerator = new ConverterGenerator(
				context,
				convertersStorageService,
				_staticAnalysisService);
			var dependencyInjectionAutowiringGenerator = new DependencyInjectionAutowiringGenerator(
				context,
				convertersStorageService,
				_staticAnalysisService);

			var convertingServiceInfo = _staticAnalysisService.GetConvertingServiceInfo();

			foreach (var convertMethodCall in _convertMethodCalls.Get())
			{
				if (semanticAnalysisService.MethodOf(convertMethodCall, convertingServiceInfo.InterfaceName) == false) // TODO: Consider namespace here
				{
					continue;
				}

				convertersStorageService.StoreConverter(convertMethodCall);
			}

			converterGenerator.AddGenerators();
			dependencyInjectionAutowiringGenerator.AddAutowiring();
		}

		public void Initialize(GeneratorInitializationContext context)
		{
			//Debugger.Launch();

			var proxyReceiver = new ProxyReceiver();

			proxyReceiver.RegisterSubscription(new ConvertCallsSyntaxReceiver(_convertMethodCalls));
			proxyReceiver.RegisterSubscription(new AutomappingWiringEntrypointSyntaxReceiver(_entryPointAttribtues));

			context.RegisterForSyntaxNotifications(() => proxyReceiver);
		}
	}
}
