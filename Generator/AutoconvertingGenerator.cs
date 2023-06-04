using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Exceptions;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Pipes;
using Paukertj.Autoconverter.Generator.Receivers.Proxy;
using Paukertj.Autoconverter.Generator.Services.Builder;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage;
using Paukertj.Autoconverter.Generator.Services.Test;
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

            _builderService.AddServices<ICodeGeneratingPipe>();
            _builderService.AddServices<ICompilationPipe>();
            _builderService.AddServices<ISyntaxReceiverPipe>();
            _builderService.AddServices<ITestService>();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                var compilationPipes = _builderService.GetServices<ICompilationPipe>();

                foreach (var compilationPipe in compilationPipes)
                {
                    compilationPipe.OnCompilation(context.Compilation);
                }

                var codeGeneratingPipes = _builderService.GetServices<ICodeGeneratingPipe>();

                foreach (var codeGeneratingPipe in codeGeneratingPipes)
                {
                    string fileName = codeGeneratingPipe.GetFileName();
                    string sourceCode = codeGeneratingPipe.GetSourceCode();

                    context.AddSource(fileName, sourceCode);
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

            var syntaxReceiverPipes = _builderService.GetServices<ISyntaxReceiverPipe>();

            foreach (var syntaxReceiverPipe in syntaxReceiverPipes)
            {
                proxyReceiver.RegisterSubscription(syntaxReceiverPipe);
            }
        }
    }
}
