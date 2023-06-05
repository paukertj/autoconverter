using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using Paukertj.Autoconverter.Generator.Exceptions;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using Paukertj.Autoconverter.Primitives.Services.Converting;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Paukertj.Autoconverter.Generator.Services.StaticAnalysis
{
    internal class StaticAnalysisService : IStaticAnalysisService
    {
        private EntryPointInfo _entryPointInfo;
        private ConvertingServiceInfo _convertingServiceInfo;
        private ConverterServiceInfo _converterServiceInfo;

        private readonly ISyntaxNodeStorageService<GenericNameSyntax> _convertMethodCalls;
        private readonly ISyntaxNodeStorageService<AttributeSyntax> _wiringEntrypointAttributes;

        //public StaticAnalysisService(
        //    ISyntaxNodeStorageService<GenericNameSyntax> convertMethodCalls,
        //    ISyntaxNodeStorageService<AttributeSyntax> wiringEntrypointAttributes)
        //{
        //    _convertMethodCalls = convertMethodCalls;
        //    _wiringEntrypointAttributes = wiringEntrypointAttributes;
        //}

        public EntryPointInfo GetEntryPointInfo()
        {
            if (_entryPointInfo != null)
            {
                return _entryPointInfo;
            }

            var entryPoints = _wiringEntrypointAttributes.Get();

            if (entryPoints.Count <= 0)
            {
                throw new ThereIsNoEntryPointException("Unable to find entry point method");
            }

            if (entryPoints.Count > 1)
            {
                throw new ThereAreMoreThanOneEntryPointException($"There are '{entryPoints.Count}' entry points but only one expected");
            }

            var entryPoint = entryPoints.First();

            var entryPointMethod = entryPoint.GetFirstParentNode<MethodDeclarationSyntax>();

            if (entryPointMethod == null)
            {
                throw new InvalidEntryPointException($"Can not get '{nameof(MethodDeclarationSyntax)}' from declared entry point");
            }

            var entryPointClass = GetClassOrRecord(entryPoint);

            if (entryPointClass == null)
            {
                throw new InvalidEntryPointException($"Can not get '{nameof(ClassDeclarationSyntax)}' or '{nameof(RecordDeclarationSyntax)}' from declared entry point");
            }

            var entryPointNamespace = GetNamespace(entryPointClass);

            if (entryPointNamespace == null)
            {
                throw new InvalidEntryPointException($"Can not get '{nameof(NamespaceDeclarationSyntax)}' or '{nameof(FileScopedNamespaceDeclarationSyntax)}' from declared entry point");
            }

            _entryPointInfo = new EntryPointInfo(
                entryPointMethod.Identifier.ValueText
                    .Trim(),
                entryPointClass.Identifier.ValueText
                    .Trim(),
                entryPointNamespace.Name
                    .ToFullString()
                    .Trim());

            return _entryPointInfo;
        }

        public ConvertingServiceInfo GetConvertingServiceInfo()
        {
            if (_convertingServiceInfo != null)
            {
                return _convertingServiceInfo;
            }

            _convertingServiceInfo = new ConvertingServiceInfo(nameof(IConvertingService), typeof(IConvertingService).Namespace);

            return _convertingServiceInfo;
        }

        public ConverterServiceInfo GetConverterServiceInfo()
        {
            if (_converterServiceInfo != null)
            {
                return _converterServiceInfo;
            }

            _converterServiceInfo = new ConverterServiceInfo(nameof(IConverter<object, object>), typeof(IConverter<object, object>).Namespace);

            return _converterServiceInfo;
        }

        public string GetClassOrRecordNesteadName(SyntaxNode entrySyntaxNode)
        {
            if (entrySyntaxNode == null)
            {
                return string.Empty;
            }

            var closestClassOrRecord = GetClassOrRecord(entrySyntaxNode);

            if (closestClassOrRecord == null)
            {
                return string.Empty;
            }

            var above = GetClassOrRecordNesteadName(closestClassOrRecord.Parent);

            var chr = string.IsNullOrWhiteSpace(above) 
                ? string.Empty 
                : ".";

            return above + chr + closestClassOrRecord.Identifier.ValueText;
		}

        public TypeDeclarationSyntax GetClassOrRecord(SyntaxNode entrySyntaxNode)
        {
            return GetOneOfNodes<ClassDeclarationSyntax, RecordDeclarationSyntax, TypeDeclarationSyntax>(entrySyntaxNode);
        }

        public BaseNamespaceDeclarationSyntax GetNamespace(SyntaxNode entrySyntaxNode)
        {
            return GetOneOfNodes<NamespaceDeclarationSyntax, FileScopedNamespaceDeclarationSyntax, BaseNamespaceDeclarationSyntax>(entrySyntaxNode);
        }

        private TBaseNode GetOneOfNodes<TNode1, TNode2, TBaseNode>(SyntaxNode entrySyntaxNode)
            where TNode1 : SyntaxNode
            where TNode2 : SyntaxNode
            where TBaseNode : SyntaxNode
        {
            if (entrySyntaxNode == null)
            {
                return null;
            }

            var syntaxNode = entrySyntaxNode.GetFirstParentNode<TNode1>();

            if (syntaxNode != null)
            {
                return syntaxNode as TBaseNode;
            }

            return entrySyntaxNode.GetFirstParentNode<TNode2>() as TBaseNode;
        }
    }
}
