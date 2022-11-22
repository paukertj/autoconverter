using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Extensions;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using Paukertj.Autoconverter.Primitives.Attributes;

namespace Paukertj.Autoconverter.Generator.Receivers
{
	internal class AutoconverterWiringEntrypointSyntaxReceiver : SyntaxNodeReceiverBase<AttributeSyntax>
	{
		public AutoconverterWiringEntrypointSyntaxReceiver(ISyntaxNodeStorageService<AttributeSyntax> syntaxNodeStorageService)
			: base(syntaxNodeStorageService, GetIndetifier)
		{

		}

		private static bool GetIndetifier(AttributeSyntax attributeSyntax)
		{
			if (attributeSyntax?.Name is not IdentifierNameSyntax identifierNameSyntax)
			{
				return false;
			}

			return identifierNameSyntax.Identifier.ValueText.AttributeEquals(nameof(AutoconverterWiringEntrypointAttribute));
		}
	}
}
