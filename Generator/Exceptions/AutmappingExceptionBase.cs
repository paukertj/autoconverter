using System;

namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal abstract class AutmappingExceptionBase : Exception
	{
		protected abstract ExceptionTypes Type { get; }

		public AutmappingExceptionBase(string message) : base(message)
		{

		}

		public string GetCode()
		{
			return $"AM{(int)Type:D4}";
		}

		public string GetHelpLinkUri()
		{
			return $"https://github.com/paukertj/autoconverter/tree/main/Docs/Errors/{GetCode()}.md";
		}
	}
}
