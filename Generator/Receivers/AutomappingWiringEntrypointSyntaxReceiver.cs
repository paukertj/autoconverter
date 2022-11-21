using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using Paukertj.Autoconverter.Primitives.Attributes;

namespace Paukertj.Autoconverter.Generator.Receivers
{
	internal class AutomappingWiringEntrypointSyntaxReceiver : SyntaxNodeReceiverBase<AttributeSyntax>
	{
		private static readonly string _attributeLongName = nameof(AutoconverterWiringEntrypointAttribute);
		private static readonly string _attributeShortName = _attributeLongName.TrimEnd("Attribute");

		public AutomappingWiringEntrypointSyntaxReceiver(ISyntaxNodeStorageService<AttributeSyntax> syntaxNodeStorageService)
			: base(syntaxNodeStorageService, GetIndetifier)
		{

		}

		private static bool GetIndetifier(AttributeSyntax attributeSyntax)
		{
			if (attributeSyntax?.Name is not IdentifierNameSyntax identifierNameSyntax)
			{
				return false;
			}

			return
				identifierNameSyntax.Identifier.ValueText == _attributeLongName ||
				identifierNameSyntax.Identifier.ValueText == _attributeShortName;
		}
	}
}
