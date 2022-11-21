using Paukertj.Autoconverter.Primitives.Services.Converter;

namespace Paukertj.Autoconverter.Demo
{
	public sealed record InfrastructureObject
	{
		//public string FirstName { get; set; }
		//public string LastName { get; set; }
	}

	//public class InfrastructureObjectToDomainObject : IConverter<InfrastructureObject, DomainObject>
	//{
	//	public DomainObject Convert(InfrastructureObject from)
	//	{
	//		if (from == null)
	//		{
	//			throw new ArgumentNullException(nameof(from));
	//		}

	//		return new DomainObject
	//		{
	//			FirstName = from.FirstName,
	//			LastName = from.LastName
	//		};
	//	}
	//}

}
