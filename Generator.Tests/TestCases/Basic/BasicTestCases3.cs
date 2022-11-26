﻿using FluentAssertions;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Abstraction.TestCases.Basic.Composition3;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases.Basic
{
	[TestFixture]
    [Description("Basic tests for object nesting")]
    public class BasicTestCases3 : TestCasesBase<IBasicTestCasesService3, BasicTestCasesComposition3>
	{
		public BasicTestCases3() : base(@".\TestCases\Basic\BasicTestCasesComposition3.cs")
		{ }

		[Test]
		public void BasicTestCasesComposition3Scenario1()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicFromEntity = service.GetSourceScenario1();

			var basicToEntity = service.ConvertScenario1();

			basicToEntity.Level1?
				.Should()
				.NotBeNull();
			basicToEntity.Level1?.Level2?
				.Should()
				.NotBeNull();
			basicToEntity.Level1?.Level2?.Level3?
				.Should()
				.NotBeNull();
			basicToEntity.Level1?.Level2?.Level3?.Level4?
				.Should()
				.Be(basicFromEntity?.Level1?.Level2?.Level3?.Level4)
				.And
				.NotBeNull();
		}

		[Test]
		public void BasicTestCasesComposition3Scenario2()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicToEntity = service.ConvertScenario2();

			basicToEntity.Level1?
				.Should()
				.NotBeNull();
			basicToEntity.Level1?.Level2?
				.Should()
				.NotBeNull();
			basicToEntity.Level1?.Level2?.Level3?
				.Should()
				.NotBeNull();
			basicToEntity.Level1?.Level2?.Level3?
				.Should()
				.BeNull();
		}

		[Test]
		public void BasicTestCasesComposition3Scenario3()
		{
			Diagnostic
				.Should()
				.BeEmpty();

			var service = GetTestCaseService();

			var basicToEntity = service.ConvertScenario3();

			basicToEntity.Level1?
				.Should()
				.BeNull();
		}
	}
}
