namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal class ThereIsNoEntryPointException : AutmappingExceptionBase
	{
		public override ExceptionTypes Type => ExceptionTypes.ThereIsNoEntryPoint;

		public ThereIsNoEntryPointException(string message) : base(message)
		{

		}
	}
}
