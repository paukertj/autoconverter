namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal class InvalidEntryPointException : AutmappingExceptionBase
	{
		protected override ExceptionTypes Type => ExceptionTypes.InvalidEntryPoint;

		public InvalidEntryPointException(string message) : base(message)
		{

		}
	}
}
