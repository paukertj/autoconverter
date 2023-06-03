using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.Builder
{
    internal interface IBuilderService
    {
        void AddServices<T>();

        IEnumerable<T> GetServices<T>();
    }
}
