using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Repositories.SyntaxNodes;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal sealed class ClassGeneratorPipe : IGeneratorConverter, IGeneratorDependencyInjectionRegistering
    {
        private readonly ISyntaxNodesRepository<GenericNameSyntax> _syntaxNodesRepository;

        public ClassGeneratorPipe(ISyntaxNodesRepository<GenericNameSyntax> syntaxNodesRepository)
        {
            _syntaxNodesRepository = syntaxNodesRepository;
        }

        public IEnumerable<StatementSyntax> GetDependencyInjectionRegistrations()
        {
            throw new NotImplementedException();
        }

        public string GetFileName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StatementSyntax> GetGeneratorImplementation()
        {
            var convertCallNodes = _syntaxNodesRepository
                .Where(n => n.Identifier.ValueText == nameof(IConvertingService.Convert));
        }
    }
}
