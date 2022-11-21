namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal class ThereIsNoEntryPointException : AutmappingExceptionBase
	{
		protected override ExceptionTypes Type => ExceptionTypes.ThereIsNoEntryPoint;

		public ThereIsNoEntryPointException(string message) : base(message)
		{

		}
	}
}
