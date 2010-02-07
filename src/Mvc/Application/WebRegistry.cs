namespace Mvc.Application
{
	using System.Reflection;
	using System.Web.Mvc;
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using DynamicServices;
	using DynamicServices.Mvc;
	using Models;

	public class WebRegistry
	{
		public static void Register(IWindsorContainer container)
		{
			RegisterControllers(container);
			RegisterControllerFactory(container);
			container.Register(Component.For<IActionInvoker>().ImplementedBy<DynamicActionInvoker>());
			RegisterRepositories(container);
		}

		private static void RegisterRepositories(IWindsorContainer container)
		{
			container.Register(
				Component.For<IDynamicRepository<Product>>().ImplementedBy<ProductRepository>().LifeStyle.Singleton);
		}

		private static void RegisterControllerFactory(IWindsorContainer container)
		{
			container.Register(Component
			                   	.For<DynamicControllerFactory>()
			                   	.LifeStyle.Singleton);

			var factory = container.Resolve<DynamicControllerFactory>();

			ControllerBuilder.Current.SetControllerFactory(factory);
		}

		private static void RegisterControllers(IWindsorContainer container)
		{
			container.Register(
				AllTypes.Of<IController>()
					.FromAssembly(Assembly.GetExecutingAssembly())
					.Configure(c => c.LifeStyle.Transient)
				);
		}
	}
}