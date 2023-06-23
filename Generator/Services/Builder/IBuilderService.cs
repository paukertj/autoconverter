using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.Builder
{
    internal interface IBuilderService
    {
        IBuilderService AddTransients<T>(params object[] args);

        IBuilderService AddSingletons<T>(params object[] args);

        T GetService<T>();

        IEnumerable<T> GetServices<T>();
    }
}
