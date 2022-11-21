using Microsoft.Extensions.DependencyInjection;
using Paukertj.Autoconverter.Primitives.Attributes;
using Paukertj.Autoconverter.Primitives.Extensions;

namespace Paukertj.Autoconverter.Demo
{
	public static partial class DiCompositorAutomapping
	{
		public static IServiceCollection AddAutomapping(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddAutoconverter();
			serviceCollection.AddAutomappingInternal();

			return serviceCollection;
		}

		[AutoconverterWiringEntrypoint]
		static partial void AddAutomappingInternal(this IServiceCollection serviceCollection);
	}
}
