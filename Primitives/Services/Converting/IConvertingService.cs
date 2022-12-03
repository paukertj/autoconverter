using System.Collections.Generic;

namespace Paukertj.Autoconverter.Primitives.Services.Converting
{
	public interface IConvertingService
	{
		TTo Convert<TFrom, TTo>(TFrom from);

		IList<TTo> Convert<TFrom, TTo>(IEnumerable<TFrom> from);

        IReadOnlyCollection<TTo> Convert<TFrom, TTo>(IReadOnlyCollection<TFrom> from);

		IReadOnlyList<TTo> Convert<TFrom, TTo>(IReadOnlyList<TFrom> from);
	}
}
