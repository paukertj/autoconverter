using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paukertj.Autoconverter.Generator.Tests.Exceptions
{
    public class TestSetupException : Exception
    {
        public TestSetupException(string message) : base(message)
        {

        }
    }
}
