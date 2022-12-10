using FluentAssertions;
using Microsoft.CodeAnalysis;
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
    [TestFixture]
    [Description("Basic tests for structures")]
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

            basicToEntity.Id
                .Should()
                .Be(basicFromEntity.Id);
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

            basicToEntity.Id
                .Should()
                .Be(basicFromEntity.Id);
        }

        [Test]
        public void BasicTestCasesComposition6Scenario3()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario3();

            var basicToEntity = service.ConvertScenario3();

            basicToEntity.Id
                .Should()
                .Be(basicFromEntity.Id);
        }

        [Test]
        public void BasicTestCasesComposition6Scenario4()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario4();

            var basicToEntity = service.ConvertScenario4();

            basicToEntity.Id
                .Should()
                .Be(basicFromEntity.Id);
        }

        [Test]
        public void BasicTestCasesComposition6Scenario5()
        {
            Diagnostic
                .Should()
                .BeEmpty();

            var service = GetTestCaseService();

            var basicFromEntity = service.GetSourceScenario5();

            var basicToEntity = service.ConvertScenario5();

            basicToEntity.Id
                .Should()
                .Be(basicFromEntity.Id);
        }
    }
}
