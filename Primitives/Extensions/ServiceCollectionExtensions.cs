using Microsoft.Extensions.DependencyInjection;
using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Primitives.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddAutoconverter(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IConvertingService, ConvertingService>();

			return serviceCollection;
		}
	}
}
