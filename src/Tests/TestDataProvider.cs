using System;
using System.Linq;
using Castle.Windsor;
using DynamicServices.Configuration;
using Tests.Repositories;

namespace Tests
{
	public class TestDataProvider : IDataProvider
	{
		public IWindsorContainer Container { get; set; }

		public object GetAllAsQueryable(Type type)
		{
			var repository = Container.Resolve(typeof(IRepository<>).MakeGenericType(type));
			return repository.GetType().GetProperty("All").GetValue(repository, null);
		}
	}
}