using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Services.ResolverAnalysis.Resolving;
using Paukertj.Autoconverter.Generator.Services.SemanticAnalysis;
using Paukertj.Autoconverter.Generator.Services.StaticAnalysis;
using Paukertj.Autoconverter.Primitives.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.ResolverAnalysis
{
    internal class ResolverAnalysisService : IResolverAnalysisService
    {
        private readonly Dictionary<string, List<ResolvingInfoBase>> _resolversStore = new Dictionary<string, List<ResolvingInfoBase>>();
        private readonly ISemanticAnalysisService _semanticAnalysisService;
        private readonly IStaticAnalysisService _staticAnalysisService;

        public ResolverAnalysisService(/*ISemanticAnalysisService semanticAnalysisService*/)
        {
            //_semanticAnalysisService = semanticAnalysisService;
        }

        public void StoreResolver(MethodDeclarationSyntax methodDeclarationSyntax)
        {
            var resolvingActions = methodDeclarationSyntax
                .DescendantNodes()
                .OfType<MemberAccessExpressionSyntax>()
                .Where(i => i.Expression is InvocationExpressionSyntax)
                .ToList();

            foreach (var resolvingAction in resolvingActions)
            {
                StoreResolvingInfo(resolvingAction);
            }

            return;
        }

        private void StoreResolvingInfo(MemberAccessExpressionSyntax memberAccessExpressionSyntax)
        {
            if (memberAccessExpressionSyntax?.Name == null)
            {
                return;
            }

            switch (memberAccessExpressionSyntax.Name.Identifier.Text) 
            {
                case nameof(ConversionResolver<object, object>.Resolve):
                    StoreResolvingInfoResolve(memberAccessExpressionSyntax);
                    break;
                case nameof(ConversionResolver<object, object>.Ignore):
                    StoreResolvingInfoIgnore(memberAccessExpressionSyntax);
                    break;
            }
        }

        private void StoreResolvingInfoResolve(MemberAccessExpressionSyntax memberAccessExpressionSyntax)
        {
            var identifiers = memberAccessExpressionSyntax
                .ChildNodes()
                .OfType<IdentifierNameSyntax>()
                .ToList();

            if (identifiers.Count != 1)
            {
                throw new Exception($"Unexpected count of identifiers, only single identifier expected but '{identifiers}' received"); // TODO
            }

            string ignoredMember = identifiers
                .First()
                .Identifier
                .ToFullString();

            var resolvingInfoIgnore = new ResolvingInfoIgnore(ignoredMember);

            Store(resolvingInfoIgnore, memberAccessExpressionSyntax);
        }

        private void StoreResolvingInfoIgnore(MemberAccessExpressionSyntax memberAccessExpressionSyntax)
        {
            return;
        }

        private void Store(ResolvingInfoBase resolvingInfo, MemberAccessExpressionSyntax memberAccessExpressionSyntax)
        {
            if (resolvingInfo == null)
            {
                throw new ArgumentNullException(nameof(resolvingInfo));
            }

            if (memberAccessExpressionSyntax == null)
            {
                throw new ArgumentNullException(nameof(memberAccessExpressionSyntax));
            }

            var identifier = GetIdentifier(memberAccessExpressionSyntax);

            List<ResolvingInfoBase> resolvers;

            if (_resolversStore.TryGetValue(identifier, out resolvers) == false)
            {
                resolvers = new List<ResolvingInfoBase>();
                _resolversStore.Add(identifier, resolvers);
            }

            resolvers.Add(resolvingInfo);
        }

        private string GetIdentifier(MemberAccessExpressionSyntax memberAccessExpressionSyntax)
        {
         
            return "todo"; // TODO
        }
    }
}
