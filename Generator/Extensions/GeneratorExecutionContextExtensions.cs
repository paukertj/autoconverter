using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Exceptions;
using System;

namespace Paukertj.Autoconverter.Generator.Extensions
{
	internal static class GeneratorExecutionContextExtensions
	{
		internal static void ReportDiagnostic(this GeneratorExecutionContext context, AutmappingExceptionBase exception)
		{
            exception
                .GetDiagnosticDescriptor()
                .ReportDiagnostic(context);
        }

        internal static void ReportDiagnostic(this GeneratorExecutionContext context, Exception exception)
        {
			exception
				.GetDiagnosticDescriptor()
				.ReportDiagnostic(context);
        }

		private static void ReportDiagnostic(this DiagnosticDescriptor diagnosticDescriptor, GeneratorExecutionContext context)
		{
            var diagnostic = Diagnostic.Create(diagnosticDescriptor, null);

            context.ReportDiagnostic(diagnostic);
        }
	}
}
