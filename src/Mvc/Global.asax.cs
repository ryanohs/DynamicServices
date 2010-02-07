namespace Mvc
{
	using System.Web;
	using System.Web.Routing;
	using Application;

	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			RouteRegistry.RegisterRoutes(RouteTable.Routes);
			ViewEngineRegistry.SetViewEngines();

			if (!WindsorContainerSetup.InitializeContainer())
			{
				return;
			}

			WebRegistry.Register(WindsorContainerSetup.Container);
			DynamicRegistry.Register(WindsorContainerSetup.Container);
			DynamicActions.RegisterConventions();
		}
	}
}