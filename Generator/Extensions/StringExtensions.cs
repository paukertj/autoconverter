namespace Paukertj.Autoconverter.Generator.Extensions
{
	internal static class StringExtensions
	{
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
	}
}
