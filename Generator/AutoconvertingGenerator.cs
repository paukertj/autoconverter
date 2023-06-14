using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Code;
using Paukertj.Autoconverter.Generator.Contexts;
using Paukertj.Autoconverter.Generator.Exceptions;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Pipes;
using Paukertj.Autoconverter.Generator.Receivers.Proxy;
using Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes;
using Paukertj.Autoconverter.Generator.Services.Builder;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using System;
using System.Linq;

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

            _builderService.AddSingletons<ISyntaxNodesRepository<GenericNameSyntax>>();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                _builderService.AddSingletons<ISemanticAnalysisService>(context);

                var staticAnalysisService = _builderService.GetServices<IStaticAnalysisService>().First();

                var entryPoint = staticAnalysisService.GetEntryPointInfo();

                var dependencyInjectionRegistrations = _builderService
                    .GetServices<IGeneratorDependencyInjectionRegistering>()
                    .SelectMany(s => s.GetDependencyInjectionRegistrations());

                var dependencyInjectionWiring =
                    SyntaxFactory.CompilationUnit()
                        .WithNamespace(
                            entryPoint.NamespaceName,
                            AutoconverterSyntaxFactory
                                .DependencyInjectionRegistrationClass(
                                    entryPoint,
                                    AutoconverterSyntaxFactory
                                        .DependencyInjectionRegistrationExtension(
                                            entryPoint, 
                                            dependencyInjectionRegistrations
                                        )
                                    )
                            );
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

            var repositories = _builderService.GetServices<ISyntaxNodesRepository<GenericNameSyntax>>();

            foreach (var repository in repositories)
            {
                proxyReceiver.RegisterSubscription(repository);
            }

            context.RegisterForSyntaxNotifications(() => proxyReceiver);
        }
    }
}
