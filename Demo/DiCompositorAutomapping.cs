using Microsoft.Extensions.DependencyInjection;
using Paukertj.Autoconverter.Primitives.Attributes;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Demo
{
	public static partial class DiCompositorAutomapping
	{
		public static IServiceCollection AddAutomapping(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IConvertingService, ConvertingService>();
			serviceCollection.AddAutomappingInternal();

			return serviceCollection;
		}

		[AutomappingWiringEntrypoint]
		static partial void AddAutomappingInternal(this IServiceCollection serviceCollection);
	}
}
