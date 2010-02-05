using System;
using Castle.Windsor;
using DynamicServices.Repositories;

namespace DynamicServices.Pipeline
{
	public class ServiceInvoker : IServiceInvoker
	{
		public IWindsorContainer Container { get; set; }

		public object GetQueryableDataFor(Type type)
		{
			var repositoryType = typeof(IRepository<>);
			var targetRepositoryType = repositoryType.MakeGenericType(type);
			var repository = Container.Resolve(targetRepositoryType);
			return repository.GetType().GetProperty("All").GetValue(repository, null);
		}
	}
}