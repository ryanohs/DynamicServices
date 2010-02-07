namespace Mvc.Application
{
	using System.Web.Mvc;
	using System.Web.Routing;

	public class RouteRegistry
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new {controller = "Products", action = "ThrowException", id = ""} // Parameter defaults
				);
		}
	}
}