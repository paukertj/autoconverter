using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Exceptions;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Pipes;
using Paukertj.Autoconverter.Generator.Receivers.Proxy;
using Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes;
using Paukertj.Autoconverter.Generator.Services.Builder;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using System;

namespace Paukertj.Autoconverter.Generator
{
    [Generator]
    internal class AutoconvertingGenerator : ISourceGenerator
    {
        private readonly IBuilderService _builderService;

        public AutoconvertingGenerator()
        {
            _builderService = new BuilderService();

            _builderService.AddSingletons<IGeneratorConverter>();
            _builderService.AddSingletons<IGeneratorDependencyInjectionRegistering>();
            _builderService.AddSingletons<IStaticAnalysisService>();

            _builderService.AddSingletons<ISyntaxNodesRepository>();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                _builderService.AddSingletons<ISemanticAnalysisService>(context);

                var codeGeneratingPipes = _builderService.GetServices<IGeneratorDependencyInjectionRegistering>();

                foreach (var codeGeneratingPipe in codeGeneratingPipes)
                {
                    var registrations = codeGeneratingPipe.GetDependencyInjectionRegistrations();
                    //string fileName = codeGeneratingPipe.GetFileName();
                    //string sourceCode = codeGeneratingPipe.GetSourceCode();

                    //context.AddSource(fileName, sourceCode);
                }
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
            var proxyReceiver = new ProxyReceiver();

            var repositories = _builderService.GetServices<ISyntaxNodesRepository>();

            foreach (var repository in repositories)
            {
                proxyReceiver.RegisterSubscription(repository);
            }

            context.RegisterForSyntaxNotifications(() => proxyReceiver);
        }
    }
}
