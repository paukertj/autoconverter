using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Code;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Pipes;
using Paukertj.Autoconverter.Generator.Services.Builder;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.SourceCodeGenerating
{
    internal sealed class SourceCodeGeneratingService : ISourceCodeGeneratingService
    {
        private const string AutoconverterServiceRegistrationClassName = "AutoconverterServiceRegistration";

        private readonly GeneratorExecutionContext _context;
        private readonly IBuilderService _builderService;
        private readonly IStaticAnalysisService _staticAnalysisService;

        public SourceCodeGeneratingService(
            GeneratorExecutionContext context, 
            IBuilderService builderService, 
            IStaticAnalysisService staticAnalysisService)
        {
            _context = context;
            _builderService = builderService;
            _staticAnalysisService = staticAnalysisService;
        }

        public void AddServiceRegistration()
        {
            var entryPoint = _staticAnalysisService.GetEntryPointInfo();

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


            var sourceCode = GenerateSourceCodeFromCompilationUnitSyntax(dependencyInjectionWiring);
            var fileName = AutoconverterServiceRegistrationClassName.GetFileName();

            _context.AddSource(fileName, sourceCode);
        }

        private string GenerateSourceCodeFromCompilationUnitSyntax(CompilationUnitSyntax compilationUnitSyntax)
        {
            return compilationUnitSyntax
                .NormalizeWhitespace()
                .SyntaxTree
                .GetText()
                .ToString();
        }
    }
}
