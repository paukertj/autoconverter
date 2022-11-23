using System;

namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition6
{
    public class BasicFromEntity6Scenario1
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class BasicFromEntity6Scenario2
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }
    }
}
