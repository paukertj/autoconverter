using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Paukertj.Autoconverter.Generator.Services.AutoconverterPropertyIgnore
{
    internal interface IAutoconverterPropertyIgnoreService
    {
        IEnumerable<string> IgnoredForTypes(IPropertySymbol propertySymbol);
    }
}
