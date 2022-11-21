namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal class InvalidEntryPointException : AutmappingExceptionBase
	{
		public override ExceptionTypes Type => ExceptionTypes.InvalidEntryPoint;

		public InvalidEntryPointException(string message) : base(message)
		{

		}
	}
}
