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

		public IQueryable<T> GetAll<T>()
		{
			var repository = Container.Resolve<IRepository<T>>();
			return repository.All;
		}
	}
}