using Microsoft.CodeAnalysis;
using System;

namespace Paukertj.Autoconverter.Generator.Extensions
{
	internal static class GeneratorExecutionContextExtensions
	{
		internal static void ReportDiagnostic<TException>(this GeneratorExecutionContext context, TException exception)
			where TException : Exception
		{
			var description = exception.GetDiagnosticDescriptor();

			var diagnostic = Diagnostic.Create(description, null);

			context.ReportDiagnostic(diagnostic);
		}
	}
}
