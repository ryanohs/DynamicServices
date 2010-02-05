namespace JoinedFilter.Windsor
{
	using System.Web.Mvc;
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;

	public class JoinedFilterRegistry
	{
		public static void Register(IWindsorContainer container)
		{
			container.Register(
				Component.For<IFilterInjector>().ImplementedBy<WindsorFilterInjector>().LifeStyle.Transient);
			container.Register(
				Component.For<IMasterFilterLocator>().ImplementedBy<MasterFilterLocator>().LifeStyle.Transient);
			container.Register(
				Component.For<IFilterLocator>().ImplementedBy<JoinedFilterLocator>().LifeStyle.Transient);
			container.Register(
				Component.For<IActionInvoker>().ImplementedBy<LocatorActionInvoker>().LifeStyle.Transient);
		}
	}
}