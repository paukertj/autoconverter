using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;

namespace Paukertj.Autoconverter.Generator.Receivers
{
	internal class AutomappingWiringEntrypointSyntaxReceiver : SyntaxNodeReceiverBase<AttributeSyntax>
	{
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
				identifierNameSyntax.Identifier.ValueText == "AutomappingWiringEntrypoint" ||
				identifierNameSyntax.Identifier.ValueText == "AutomappingWiringEntrypointAttribute";
		}
	}
}
