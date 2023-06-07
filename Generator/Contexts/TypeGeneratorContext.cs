namespace Paukertj.Autoconverter.Generator.Contexts
{
    public class TypeGeneratorContext
    {
        /// <summary>
        /// Type full name, for example 'System.string', 'Paukertj.Autoconverter.Generator.Contexts.TypeGeneratorContext', etc.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Type full name that respects primitive types, for example 'System.string' will be 'string', 'System.int?' will be 'int'
        /// but 'Paukertj.Autoconverter.Generator.Contexts.TypeGeneratorContext' remains the same.
        /// </summary>
        public string PureFullName { get; } 

        /// <summary>
        /// Type full name that respects primitive types and keep nullability, for example 'System.string' will be 'string', 'System.int?' 
        /// will be 'int?' but 'Paukertj.Autoconverter.Generator.Contexts.TypeGeneratorContext' remains the same.
        /// </summary>
        public string PureFullNameNullable { get; }

        public TypeGeneratorContext(string fullName, string pureFullName, string pureFullNameNullable)
        {
            FullName = fullName;
            PureFullName = pureFullName;
            PureFullNameNullable = pureFullNameNullable;
        }
    }
}
