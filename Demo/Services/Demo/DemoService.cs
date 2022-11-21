using Paukertj.Autoconverter.Primitives.Services.Converting;

namespace Paukertj.Autoconverter.Demo.Services.Demo
{
	internal class DemoService : IDemoService
	{
		private readonly IConvertingService _convertingService;

		public DemoService(IConvertingService convertingService)
		{
			_convertingService = convertingService;
		}

		public void SomeMethod()
		{
			var domainObjectWithNesting = new DomainObjectWithNesting
			{
				Nested = new DomainObjectWithNesting.NestedDomain
				{
					IntProperty = 19
				},
				ListOfStrings = new List<string>
				{
					"a", "b", "c"
				},
				ListOfTypes = new List<DomainObjectWithNesting.NestedDomain>
				{
					new DomainObjectWithNesting.NestedDomain
					{
						IntProperty = 21
					}
				},
				StringProperty = "Some string"
			};

			var someDomainObject = new DomainObject
			{
				//FirstName = "Jiri",
				//LastName = "Paukert"
			};

			//var infrastructureObjectWithNesting = _convertingService.Convert<DomainObjectWithNesting, InfrastructureObjectWithNesting>(domainObjectWithNesting);

			var infrastructureObject = _convertingService.Convert<DomainObject, InfrastructureObject>(someDomainObject);
		}
	}
}
