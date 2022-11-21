using Paukertj.Autoconverter.Primitives.Attributes;

namespace Paukertj.Autoconverter.Demo
{
	public sealed record DomainObject
	{
		public string FirstName { get; set; }

        public string LastName { get; set; }
	}
}
