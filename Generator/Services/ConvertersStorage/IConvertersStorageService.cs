using Microsoft.CodeAnalysis.CSharp.Syntax;
using Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage
{
    public interface IConvertersStorageService
	{
		void StoreGeneratedConverter(GenericNameSyntax genericNameSyntax);

		void StoreExistingConverter(GenericNameSyntax genericNameSyntax);

		IReadOnlyList<TConverter> GetConverters<TConverter>()
			where TConverter : ConversionInfoBase;
	}
}
