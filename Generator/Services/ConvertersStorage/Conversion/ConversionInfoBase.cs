using System;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage.Conversion
{
    public abstract class ConversionInfoBase
    {
        public string FromFullName { get; }

        public string ToFullName { get; }

        public abstract string ImplementationNamespace { get; }

        public abstract string ImplementationName { get; }

        public string Id { get; }

        protected ConversionInfoBase(string fromFullName, string toFullName)
        {
            if (string.IsNullOrWhiteSpace(fromFullName))
            {
                throw new ArgumentNullException(nameof(fromFullName));
            }

            if (string.IsNullOrWhiteSpace(toFullName))
            {
                throw new ArgumentNullException(nameof(toFullName));
            }

            FromFullName = fromFullName;
            ToFullName = toFullName;

            Id = fromFullName + "->" + toFullName;
        }
    }
}
