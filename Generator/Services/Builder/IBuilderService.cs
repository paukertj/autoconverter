using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.Builder
{
    internal interface IBuilderService
    {
        void AddTransients<T>(params object[] args);

        void AddSingletons<T>(params object[] args);

        IEnumerable<T> GetServices<T>();
    }
}
