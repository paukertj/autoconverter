using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Services.SyntaxNodeStorage;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Generator.Receivers
{
	internal class ConvertCallsSyntaxReceiver : SyntaxNodeReceiverBase<GenericNameSyntax>
	{
		public ConvertCallsSyntaxReceiver(ISyntaxNodeStorageService<GenericNameSyntax> syntaxNodeStorageService)
			: base(syntaxNodeStorageService, GetIndetifier)
		{

		}

		private static bool GetIndetifier(GenericNameSyntax genericNameSyntax)
		{
			return genericNameSyntax.Identifier.ValueText == nameof(IConvertingService.Convert);
		}
	}
}
