using Microsoft.Extensions.DependencyInjection;
using Paukertj.Autoconverter.Primitives.Services.Converter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Primitives.Services.Converting
{
	public class ConvertingService : IConvertingService
	{
		private readonly IServiceProvider _serviceProvider;

		public ConvertingService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public TTo Convert<TFrom, TTo>(TFrom from)
		{
			var convertor = _serviceProvider.GetRequiredService<IConverter<TFrom, TTo>>();

			return convertor.Convert(from);
		}

		public IList<TTo> Convert<TFrom, TTo>(IEnumerable<TFrom> from)
		{
			return ConvertInternal<TFrom, TTo>(from);
		}

		public IReadOnlyCollection<TTo> Convert<TFrom, TTo>(IReadOnlyCollection<TFrom> from)
		{
			return ConvertInternal<TFrom, TTo>(from);
		}

		public IReadOnlyList<TTo> Convert<TFrom, TTo>(IReadOnlyList<TFrom> from)
		{
			return ConvertInternal<TFrom, TTo>(from);
		}

		private List<TTo> ConvertInternal<TFrom, TTo>(IEnumerable<TFrom> from)
		{
			var convertor = _serviceProvider.GetRequiredService<IConverter<TFrom, TTo>>();

			return from
				.Select(Convert<TFrom, TTo>)
				.ToList();
		}
	}
}
