namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal class ThereAreMoreThanOneEntryPointException : AutmappingExceptionBase
	{
		protected override ExceptionTypes Type => ExceptionTypes.ThereAreMoreThanOneEntryPoint;

		public ThereAreMoreThanOneEntryPointException(string message) : base(message)
		{

		}
	}
}
