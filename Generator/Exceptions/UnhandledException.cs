namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal class UnhandledException : AutmappingExceptionBase
	{
		protected override ExceptionTypes Type => ExceptionTypes.Unhandled;

		public UnhandledException(string message) : base(message)
		{

		}
	}
}
