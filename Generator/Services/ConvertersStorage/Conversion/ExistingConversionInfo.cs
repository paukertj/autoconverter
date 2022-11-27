namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion
{
    internal class ExistingConversionInfo : ConversionInfoBase
    {
        public override string ImplementationNamespace { get; }

        public override string ImplementationName { get; }

        public ExistingConversionInfo(string fromFullName, string toFullName, string implementationNamespace, string implementationName) 
            : base(fromFullName, toFullName)
        {
            ImplementationNamespace = implementationNamespace;
            ImplementationName = implementationName;
        }
    }
}
