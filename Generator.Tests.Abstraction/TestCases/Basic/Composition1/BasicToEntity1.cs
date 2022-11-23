namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition1
{
    public class BasicToEntity1Scenario1
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

    public record BasicToEntity1Scenario2
    {
        public decimal Decimal { get; init; }

        public ulong UnsingnedLong { get; init; }

        public long Long { get; init; }

        public uint UnsignedInteger { get; init; }

        public int Integer { get; init; }

        public byte Byte { get; init; }

        public bool Bool { get; init; }

        public string String { get; init; }

        public char Char { get; init; }
    }

    public class BasicToEntity1Scenario3
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

    public record BasicToEntity1Scenario4
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
}
