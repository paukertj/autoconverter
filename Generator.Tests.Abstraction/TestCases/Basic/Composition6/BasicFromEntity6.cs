using System;

namespace Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition6
{
    public class BasicFromEntity6Scenario1
    {
        public Guid Id { get; set; }
    }

    public struct BasicFromEntity6Scenario2
    {
        public Guid Id { get; set; }
    }

    public class BasicFromEntity6Scenario3
    {
        public Guid Id { get; set; }
    }

    public struct BasicFromEntity6Scenario4
    {
        public Guid Id { get; set; }
    }

    public record BasicFromEntity6Scenario5
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? Id { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
