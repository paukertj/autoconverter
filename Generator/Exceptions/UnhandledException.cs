namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal class UnhandledException : AutmappingExceptionBase
	{
		public override ExceptionTypes Type => ExceptionTypes.Unhandled;

		public UnhandledException(string message) : base(message)
		{

		}
	}
}
