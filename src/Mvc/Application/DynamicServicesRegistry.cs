namespace Mvc.Application
{
	using System;
	using Castle.Windsor;
	using DynamicServices;
	using DynamicServices.Mvc;
	using DynamicServices.Mvc.ActionDescriptors;
	using DynamicServices.Sakurity;
	using Models;

	public class DynamicServicesRegistry : ServicesRegistry
	{
		public DynamicServicesRegistry()
		{
			Service("products").For<IDynamicRepository<Product>>();
			Service("product").Entity<Product>();
			Service("locations").For<LocationQueries>();
		}

		public static void RegisterConventions()
		{
			DynamicControllerRegistrar.AddCommonActionFor<Query>("alls");
			DynamicControllerRegistrar.AddCommonActionFor<ViewWithoutModel>("index");
		}

		public static void Bootstrap(IWindsorContainer container)
		{
			var offica = container.Resolve<ISakurityOffica>();
			var sakurity = new Sakurity();
			sakurity.ConfigyaOffia(offica);
		}
	}
}