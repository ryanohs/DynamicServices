using System.Web.Mvc;

namespace DynamicServices.Mvc
{
	public static class ControllerContextExtensions
	{
		public static string GetControllerName(this ControllerContext context)
		{
			return context.RouteData.Values["controller"].ToString().ToLowerInvariant();
		}
	}
}