using System.IO;

namespace Paukertj.Autoconverter.Generator.Tests.Extensions
{
    internal static class StringExtensions
    {
        internal static string ToPath(this string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            var fragments = path.Split('\\');

            return Path.Combine(fragments);
        }
    }
}
