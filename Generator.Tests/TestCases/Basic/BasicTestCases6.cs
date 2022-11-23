using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition5;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
    public class BasicTestCases6 : TestCasesBase<IBasicTestCasesService6, BasicTestCasesComposition6>
    {
        public BasicTestCases6() : base(@".\TestCases\Basic\BasicTestCasesComposition6.cs")
        { }

        [Test]
        public void BasicTestCasesComposition6Scenario1()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario1();

            var basicToEntity = service.ConvertScenario1();

            basicToEntity.FirstName
                .Should()
                .Be(basicFromEntity.FirstName);
            basicToEntity.LastName
                .Should()
                .Be(basicFromEntity.LastName);

            basicToEntity.Id
                .Should()
                .BeEmpty();
        }

        [Test]
        public void BasicTestCasesComposition6Scenario2()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario2();

            var basicToEntity = service.ConvertScenario2();

            basicToEntity.FirstName
                .Should()
                .Be(basicFromEntity.FirstName);
            basicToEntity.LastName
                .Should()
                .Be(basicFromEntity.LastName);
            basicToEntity.Id
                .Should()
                .Be(basicFromEntity.Id);
        }
    }
}
