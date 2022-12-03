using Microsoft.CodeAnalysis;
using Paukertj.Autoconverter.Generator.Exceptions;
using System;

namespace Paukertj.Autoconverter.Generator.Extensions
{
	internal static class ExceptionExtensions
	{
		internal static DiagnosticDescriptor GetDiagnosticDescriptor(this Exception exception)
		{
			return new DiagnosticDescriptor(
					ExceptionTypes.Unhandled.GetCode(),
					"Unhandled error occured during autoconverting",
                    exception.GetMessage(ExceptionTypes.Unhandled),
					"Autoconverter",
					DiagnosticSeverity.Error,
					true,
					helpLinkUri: ExceptionTypes.Unhandled.GetHelpLinkUri());
		}

		internal static DiagnosticDescriptor GetDiagnosticDescriptor(this AutmappingExceptionBase exception)
		{
			return new DiagnosticDescriptor(
					exception.Type.GetCode(),
					"Unable to process autoconverting",
					exception.GetMessage(exception.Type),
					"Autoconverter",
					DiagnosticSeverity.Error,
					true,
					helpLinkUri: exception.Type.GetHelpLinkUri());
		}

		private static string GetCode(this ExceptionTypes exceptionType)
		{
			return $"AC{(int)exceptionType:D4}";
		}

		private static string GetHelpLinkUri(this ExceptionTypes exceptionType)
		{
			return $"https://github.com/paukertj/autoconverter/tree/develop/Docs/Errors/{exceptionType.GetCode()}.md";
		}

        private static string GetMessage(this Exception exception, ExceptionTypes exceptionType)
        {
			return $"{exception.Message.Trim('.', '?', '!')}, see {GetHelpLinkUri(exceptionType)} for more information";
        }
    }
}
