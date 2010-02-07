namespace Mvc.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Castle.Windsor;
	using Microsoft.Practices.ServiceLocation;

	public class WindsorCsl : IServiceLocator
	{
		private readonly IWindsorContainer _Container;

		public WindsorCsl(IWindsorContainer container)
		{
			_Container = container;
		}

		public object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		public object GetInstance(Type serviceType)
		{
			return _Container.Resolve(serviceType);
		}

		public object GetInstance(Type serviceType, string key)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return _Container.ResolveAll(serviceType).OfType<object>();
		}

		public TService GetInstance<TService>()
		{
			return _Container.Resolve<TService>();
		}

		public TService GetInstance<TService>(string key)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TService> GetAllInstances<TService>()
		{
			return _Container.ResolveAll<TService>();
		}
	}
}