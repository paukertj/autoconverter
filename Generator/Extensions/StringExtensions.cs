namespace Paukertj.Autoconverter.Generator.Extensions
{
	internal static class StringExtensions
	{
		internal static string GetFileName(this string className)
		{
			return className + ".g.cs";
		}
	}
}
