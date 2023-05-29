namespace Paukertj.Autoconverter.Generator.Extensions
{
    internal static class StringExtensions
    {
        internal static string FirstUpperCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return str[0].ToString().ToUpper() + str.Substring(1);

        }

        internal static string GetFileName(this string className)
        {
            return className + ".g.cs";
        }

        internal static string TrimEnd(this string source, string value)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            int lastIndex = source.LastIndexOf(value);

            if (lastIndex < 0)
            {
                return source;
            }

            return source.Remove(lastIndex);
        }

        internal static bool AttributeEquals(this string attr1, string attr2)
        {
            if (string.IsNullOrWhiteSpace(attr1) || string.IsNullOrWhiteSpace(attr2))
            {
                return false;
            }

            string withoutSuffix = attr2.TrimEnd("Attribute");

            return
                attr1 == withoutSuffix ||
                attr1 == attr2;
        }
    }
}
