namespace Mvc.Application
{
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using DynamicServices.Mvc;

	public static class DynamicRegistry
	{
		public static void Register(IWindsorContainer container)
		{
			container.Register(
				AllTypes.Of<DynamicActionDescriptor>().FromAssembly(typeof (DynamicController).Assembly)
				);
			container.Register(
				AllTypes.Of<IFindAction>().FromAssembly(typeof (DynamicController).Assembly)
				);
			container.Register(
				Component.For<IDynamicQuery>().ImplementedBy<DynamicQuery>()
				);
		}
	}
}