using Paukertj.Autoconverter.Primitives.Services.Converter;
using Microsoft.Extensions.DependencyInjection;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases
{
	public static partial class DiCompositorAutomapping
	{
		static partial void AddAutoconverterInternal(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IConverter<Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.CustomConverter.Composition1.CustomConverterFromEntity1, Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.CustomConverter.Composition1.CustomConverterToEntity1>, Paukertj.Autoconverter.Generator.Tests.TestCases.CustomConverter.CustomConverter1>();
		}
	}
}