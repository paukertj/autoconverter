using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Exceptions;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Generators.Converter;
using Paukertj.Autoconverter.Generator.Generators.DependencyInjectionAutowiring;
using Paukertj.Autoconverter.Generator.Receivers;
using Paukertj.Autoconverter.Generator.Receivers.Proxy;
using Paukertj.Autoconverter.Generator.Services.AutoconverterPropertyIgnore;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using Paukertj.Autoconverter.Primitives.Resolvers;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;
using System.Diagnostics;

namespace Paukertj.Autoconverter.Generator
{
	[Generator]
	internal class AutoconvertingGenerator : ISourceGenerator
	{
		private readonly ISyntaxNodeStorageService<GenericNameSyntax> _convertMethodCalls;
		private readonly ISyntaxNodeStorageService<AttributeSyntax> _wiringEntrypointAttributes;
        private readonly ISyntaxNodeStorageService<AttributeSyntax> _propertyIgnoreAttributes;
		private readonly ISyntaxNodeStorageService<GenericNameSyntax> _converterImplementations;
		private readonly ISyntaxNodeStorageService<MethodDeclarationSyntax> _resolverImplementations;

        private readonly IStaticAnalysisService _staticAnalysisService;

		public AutoconvertingGenerator()
		{
			_convertMethodCalls = new SyntaxNodeStorageService<GenericNameSyntax>();
            _wiringEntrypointAttributes = new SyntaxNodeStorageService<AttributeSyntax>();
            _propertyIgnoreAttributes = new SyntaxNodeStorageService<AttributeSyntax>();
            _converterImplementations = new SyntaxNodeStorageService<GenericNameSyntax>();
            _resolverImplementations = new SyntaxNodeStorageService<MethodDeclarationSyntax>();
            _staticAnalysisService = new StaticAnalysisService(_convertMethodCalls, _wiringEntrypointAttributes);
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
            var autoconverterPropertyIgnoreService = new AutoconverterPropertyIgnoreService(_propertyIgnoreAttributes, _staticAnalysisService, semanticAnalysisService);
            var convertersStorageService = new ConvertersStorageService(semanticAnalysisService, _staticAnalysisService, autoconverterPropertyIgnoreService);
			var converterGenerator = new ConverterGenerator(
				context,
				convertersStorageService,
				_staticAnalysisService);
			var dependencyInjectionAutowiringGenerator = new DependencyInjectionAutowiringGenerator(
				context,
				convertersStorageService,
				_staticAnalysisService);

			// First process manually created converters
            foreach (var convertImplementation in _converterImplementations.Get())
            {
                if (semanticAnalysisService.MemberOf<IConverter<object, object>>(convertImplementation) == false)
                {
                    continue;
                }

                convertersStorageService.StoreExistingConverter(convertImplementation);
            }

            foreach (var resolverImplementation in _resolverImplementations.Get())
            {
                if (semanticAnalysisService.MemberOf<Resolver>(resolverImplementation) == false)
                {
                    continue;
                }

                convertersStorageService.StoreExistingConverter(convertImplementation);
            }

            // Then process conversions that needs to be generated
            foreach (var convertMethodCall in _convertMethodCalls.Get())
			{
				if (semanticAnalysisService.MemberOf<IConvertingService>(convertMethodCall) == false)
				{
					continue;
				}

				convertersStorageService.StoreGeneratedConverter(convertMethodCall);
			}

			converterGenerator.AddGenerators();
			dependencyInjectionAutowiringGenerator.AddAutowiring();
		}

		public void Initialize(GeneratorInitializationContext context)
		{
			//Debugger.Launch();

			var proxyReceiver = new ProxyReceiver();

			proxyReceiver.RegisterSubscription(new ConvertCallsSyntaxReceiver(_convertMethodCalls));
			proxyReceiver.RegisterSubscription(new AutoconverterWiringEntrypointSyntaxReceiver(_wiringEntrypointAttributes));
            proxyReceiver.RegisterSubscription(new AutoconverterPropertyIgnoreReceiver(_propertyIgnoreAttributes));
            proxyReceiver.RegisterSubscription(new ConvertImplementationsSyntaxReceiver(_converterImplementations));
			proxyReceiver.RegisterSubscription(new ResolversSyntaxReceiver(_resolverImplementations));

            context.RegisterForSyntaxNotifications(() => proxyReceiver);
		}
	}
}
