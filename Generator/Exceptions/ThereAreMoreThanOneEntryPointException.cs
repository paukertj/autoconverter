namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal class ThereAreMoreThanOneEntryPointException : AutmappingExceptionBase
	{
		public override ExceptionTypes Type => ExceptionTypes.ThereAreMoreThanOneEntryPoint;

		public ThereAreMoreThanOneEntryPointException(string message) : base(message)
		{

		}
	}
}
