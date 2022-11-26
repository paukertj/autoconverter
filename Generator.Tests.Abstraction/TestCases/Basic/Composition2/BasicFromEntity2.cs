using System;

namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition1
{
    public class BasicFromEntity2Scenario1
    {
        public decimal Decimal { get; set; }

        public ulong UnsingnedLong { get; set; }

        public long Long { get; set; }

        public uint UnsignedInteger { get; set; }

        public int Integer { get; set; }

        public byte Byte { get; set; }

        public bool Bool { get; set; }

        public string String { get; set; }

        public char Char { get; set; }
    }

    public class BasicFromEntity2Scenario2
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class BasicFromEntity2Scenario3
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }
    }
}
