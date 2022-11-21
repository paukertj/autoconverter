using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paukertj.Autoconverter.Generator.Services.ConvertersStorage
{
	public sealed class ConversionInfo
	{
		public ConversionMember From { get; }

		public ConversionMember To { get; }

		public string Id { get; }

		public bool RequireConversion => From.Properties.Any(p => p.RequireConversion) && To.Properties.Any(p => p.RequireConversion);

		public ConversionInfo(ConversionMember from, ConversionMember to)
		{
			if (from == null)
			{
				throw new ArgumentNullException(nameof(from));
			}

			if (to == null)
			{
				throw new ArgumentNullException(nameof(to));
			}

			From = from;
			To = to;

			Id = From.FullName + "->" + To.FullName;
		}
	}
}
