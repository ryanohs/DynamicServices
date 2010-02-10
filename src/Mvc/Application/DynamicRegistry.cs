namespace Mvc.Application
{
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using DynamicServices;
	using DynamicServices.Mvc;
	using DynamicServices.Pagination;
	using DynamicServices.Sakurity;

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
			container.Register(
				Component.For<ServicesRegistry>().ImplementedBy<DynamicServicesRegistry>().LifeStyle.Singleton
				);
			container.Register(
				Component.For<ISakurityOffica>().ImplementedBy<SakurityOffica>().LifeStyle.Singleton
				);
			container.Register(
				Component.For<IDynamicStage>().ImplementedBy<SakurityStage>().LifeStyle.Transient
				);
			container.Register(
				Component.For<DomainInvoker>().LifeStyle.Transient
				);
			container.Register(
				Component.For<PaginationStage>().LifeStyle.Transient
				);
		}
	}
}