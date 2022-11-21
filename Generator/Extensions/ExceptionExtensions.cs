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
					exception.Message,
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
					exception.Message,
					"Autoconverter",
					DiagnosticSeverity.Error,
					true,
					helpLinkUri: exception.Type.GetHelpLinkUri());
		}

		private static string GetCode(this ExceptionTypes exceptionType)
		{
			return $"AM{(int)exceptionType:D4}";
		}

		private static string GetHelpLinkUri(this ExceptionTypes exceptionType)
		{
			return $"https://github.com/paukertj/autoconverter/tree/main/Docs/Errors/{exceptionType.GetCode()}.md";
		}
	}
}
