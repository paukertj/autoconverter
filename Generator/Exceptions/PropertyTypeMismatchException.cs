namespace Paukertj.Autoconverter.Generator.Exceptions
{
    internal class PropertyTypeMismatchException : AutmappingExceptionBase
    {
        public override ExceptionTypes Type => ExceptionTypes.PropertyTypeMismatchException;

        public PropertyTypeMismatchException(string message) : base(message)
        {
        }
    }
}
