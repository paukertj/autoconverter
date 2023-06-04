using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.Builder
{
    internal interface IBuilderService
    {
        void AddTransients<T>();

        void AddSingletons<T>();

        IEnumerable<T> GetServices<T>();
    }
}
