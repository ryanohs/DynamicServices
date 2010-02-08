namespace DynamicServices
{
	using System.Collections.Generic;
	using System.Linq;

	public abstract class ServicesRegistry : List<DynamicService>
	{
		public DynamicService Service(string name)
		{
			var service = new DynamicService(name);
			Add(service);
			return service;
		}

		public DynamicService GetService(string name)
		{
			return this.FirstOrDefault(s => s.Name.ToLowerInvariant() == name.ToLowerInvariant());
		}
	}
}