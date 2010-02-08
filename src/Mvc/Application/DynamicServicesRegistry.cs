namespace Mvc.Application
{
	using DynamicServices;
	using DynamicServices.Mvc;
	using DynamicServices.Mvc.ActionDescriptors;
	using Models;

	public class DynamicServicesRegistry : ServicesRegistry
	{
		public DynamicServicesRegistry()
		{
			Service("products").For<IDynamicRepository<Product>>().Entity<Product>();
		}

		public static void RegisterConventions()
		{
			DynamicControllerRegistrar.AddCommonActionFor<Query>("alls");
			DynamicControllerRegistrar.AddCommonActionFor<ViewWithoutModel>("index");
		}
	}
}