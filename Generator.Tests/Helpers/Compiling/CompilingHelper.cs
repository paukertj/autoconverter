using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Paukertj.Autoconverter.Generator.Tests.Exceptions;
using Paukertj.Autoconverter.Primitives.Extensions;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Paukertj.Autoconverter.Generator.Tests.Helpers.Compiling
{
    public class CompilingHelper : ICompilingHelper
    {
        private Compilation _compilationOutput = null;
        private IReadOnlyList<Diagnostic> _diagnosticOutput = null;
        private List<string> _sourceCode = new List<string>();

        private readonly List<Assembly> _assemblies;

        private CompilingHelper()
        {
            _assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly => !assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
                .ToList();

            AddAssemblyFromType<IServiceCollection>();
            AddAssemblyFromType<IConvertingService>();
            AddAssemblyFromType<ServiceProvider>();
            AddAssemblyFromType<Exception>();

            AddAssembly(typeof(ServiceCollectionExtensions).Assembly);
        }

        public static ICompilingHelper Create()
        {
            return new CompilingHelper();
        }


        public ICompilingHelper AddSourceCode(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException($"Unable to add source code because target file does not exists! Check if path '{filePath}' is valid.");
            }

            string fileContent = File.ReadAllText(filePath);
            _sourceCode.Add(fileContent);

            return this;
        }

        public ICompilingHelper AddAssemblyFromType<TType>()
        {
            return AddAssembly(typeof(TType).Assembly);
        }

        public ICompilingHelper AddAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);

            return this;
        }

        public ICompilingHelper Compile()
        {
            if (_sourceCode.Count <= 0)
            {
                throw new TestSetupException($"There is no source code! Call '{nameof(AddSourceCode)}' first!");
            }

            var syntaxTree = _sourceCode
                .Select(s => CSharpSyntaxTree.ParseText(s))
                .ToArray();

            var references = _assemblies
                          .Select(assembly => MetadataReference.CreateFromFile(assembly.Location))
                          .Cast<MetadataReference>();

            var compilation = CSharpCompilation.Create(
                "Paukertj.Autoconverter.Tests.Live",
                syntaxTree,
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            // Source Generator to test 
            var generator = new AutoconvertingGenerator();

            CSharpGeneratorDriver.Create(generator)
                                 .RunGeneratorsAndUpdateCompilation(compilation, out var compilationOutput, out var diagnosticOutput);

            _compilationOutput = compilationOutput;
            _diagnosticOutput = diagnosticOutput;

            return this;
        }

        public IReadOnlyList<Diagnostic> GetDiagnosticOutput()
        {
            return _diagnosticOutput;
        }

        public Compilation GetCompilationOutput()
        {
            return _compilationOutput;
        }
    }
}
