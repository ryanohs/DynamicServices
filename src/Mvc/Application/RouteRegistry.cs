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
				"Extension", // Route name
				"{controller}/{action}.{ext}/{id}", // URL with parameters
				new { controller = "Products", action = "All", ext = "json", id = "" } // Parameter defaults
				);

			routes.MapRoute(
				"ExtensionLess", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new {controller = "Products", action = "All", id = ""} // Parameter defaults
				);
		}
	}
}