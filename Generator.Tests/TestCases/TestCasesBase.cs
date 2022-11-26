using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Paukertj.Autoconverter.Generator.Tests.Extensions;
using Paukertj.Autoconverter.Generator.Tests.Helpers.Compiling;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Tests.TestCases
{
	public abstract class TestCasesBase<TService, TComposition>
	{
		public IReadOnlyList<Diagnostic> Diagnostic { get; private set; }

		private ICompilingHelper _compilingHelper;

		private readonly List<string> _files = new List<string>();

		public TestCasesBase(params string[] files)
		{
			var paths = files.Select(f => f.ToPath());

			_files.AddRange(paths);
			_files.Add(@".\TestCases\TestCasesCompositionBase.cs".ToPath());
		}

		[SetUp]
		public void SetUpTest()
		{
			_compilingHelper = CompilingHelper
				.Create()
				.AddAssemblyFromType<TService>();

			foreach (var file in _files)
			{
				_compilingHelper = _compilingHelper.AddSourceCode(file);
			}

			_compilingHelper = _compilingHelper.Compile();

			Diagnostic = _compilingHelper
				.GetDiagnosticOutput();
		}

		public ServiceProvider GetServiceProvider()
		{
			return _compilingHelper
				.GetCompilationOutput()
				.Run<ServiceProvider, TComposition>();
		}

		public TService GetTestCaseService()
		{
			return GetServiceProvider()
				.GetRequiredService<TService>();
		}
	}
}
