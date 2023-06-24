using System;
using System.Collections.Generic;
using System.Text;
using Paukertj.Autoconverter.Generator.Entities;

namespace Paukertj.Autoconverter.Generator.Pipes
{
    internal interface IAnalyzer
    {
        IEnumerable<TypeConversion> GetData();
    }
}
