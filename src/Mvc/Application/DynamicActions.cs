namespace Mvc.Application
{
	using DynamicServices.Mvc;
	using DynamicServices.Mvc.ActionDescriptors;
	using Models;

	public static class DynamicActions
	{
		public static void RegisterConventions()
		{
			DynamicControllerRegistrar.AddCommonActionFor<Query>("alls");
			DynamicControllerRegistrar.AddCommonActionFor<ViewWithoutModel>("index");

			DynamicControllerRegistrar.AddCommandHandler<Product>("products");
		}
	}
}