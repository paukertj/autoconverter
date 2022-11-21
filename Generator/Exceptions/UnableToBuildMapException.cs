namespace Paukertj.Autoconverter.Generator.Exceptions
{
	internal class UnableToBuildMapException : AutmappingExceptionBase
	{
		protected override ExceptionTypes Type => ExceptionTypes.UnableToBuildMap;

		public UnableToBuildMapException(string message) : base(message)
		{

		}
	}
}
