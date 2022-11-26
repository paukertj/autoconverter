using Microsoft.Extensions.DependencyInjection;
using Paukertj.Autoconverter.Generator.Tests.Abstraction;
using Paukertj.Autoconverter.Primitives.Attributes;
using Paukertj.Autoconverter.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases
{
    public abstract class TestCasesCompositionBase<TService, TImplementation>
            where TService : class
            where TImplementation : class, TService
    {
        public ServiceProvider Main()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<TService, TImplementation>();
            Paukertj.Autoconverter.Generator.Tests.TestCases.DiCompositorAutomapping.AddComposition(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }
    }

    public static partial class DiCompositorAutomapping
    {
        public static IServiceCollection AddComposition(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoconverter();
            serviceCollection.AddAutoconverterInternal();

            return serviceCollection;
        }

        [AutoconverterWiringEntrypoint]
        static partial void AddAutoconverterInternal(this IServiceCollection serviceCollection);
    }
}
