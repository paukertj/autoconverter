using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Extensions
{
	internal static class ListExtensions
	{
		internal static void RemoveRange<T>(this IList<T> list, IList<T> toRemove)
		{
			if (list?.Any() != true || toRemove.Any() != true)
			{
				return;
			}

			foreach (var r in toRemove)
			{
				list.Remove(r);
			}
		}
	}
}
