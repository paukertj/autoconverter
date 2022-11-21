using System;

namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal abstract class AutmappingExceptionBase : Exception
	{
		public abstract ExceptionTypes Type { get; }

		public AutmappingExceptionBase(string message) : base(message)
		{

		}
	}
}
