using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage
{
	public interface IConvertersStorageService
	{
		void StoreConverter(GenericNameSyntax genericNameSyntax);

		IReadOnlyList<ConversionInfo> GetConverters();
	}
}
