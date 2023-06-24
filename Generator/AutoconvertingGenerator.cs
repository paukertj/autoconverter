using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Exceptions;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Pipes;
using Paukertj.Autoconverter.Generator.Receivers.Proxy;
using Paukertj.Autoconverter.Generator.Services.Builder;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Generator.Services.SourceCodeGenerating;
using System;
using Paukertj.Autoconverter.Generator.Services.ConversionLogic;
using Paukertj.Autoconverter.Generator.Services.AutoconverterSyntax;

namespace Paukertj.Autoconverter.Generator
{
    [Generator]
    internal class AutoconvertingGenerator : ISourceGenerator
    {
        private readonly IBuilderService _builderService;

        public AutoconvertingGenerator()
        {
            _builderService = new BuilderService()
                .AddSingletons<IGeneratorConverter>()
                .AddSingletons<IGeneratorDependencyInjectionRegistering>()
                .AddSingletons<IGeneratorDependencyInjectionRegistering>()
                .AddSingletons<IStaticAnalysisService>()
                .AddSingletons<IProxyReceiver>()
                .AddSingletons<IConversionLogicService>()
                .AddSingletons<IAutoconverterSyntaxService>();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                _builderService
                    .AddSingletons<ISemanticAnalysisService>(context);

                var sourceCodeGeneratingService = _builderService
                    .AddSingletons<ISourceCodeGeneratingService>(context)
                    .GetService<ISourceCodeGeneratingService>();

                sourceCodeGeneratingService.AddServiceRegistration();
                // TODO More generators
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

        public void Initialize(GeneratorInitializationContext context)
        {
            var proxyReceiver = _builderService
                .GetService<IProxyReceiver>()
                .RegisterRepository<GenericNameSyntax>()
                .RegisterRepository<AttributeSyntax>();

            context.RegisterForSyntaxNotifications(() => proxyReceiver);
        }
    }
}
