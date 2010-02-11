namespace DynamicServices.Mvc
{
	using System.Web;
	using System.Web.Mvc;
	using Scaffolding.JqGrid;

	public class JqGridInterceptorFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			DetectQueryForScaffoldedJqGrid(filterContext);
		}

		private void DetectQueryForScaffoldedJqGrid(ActionExecutingContext filterContext)
		{
			var action = filterContext.ActionDescriptor as ReflectedQuery;
			if (action == null)
			{
				return;
			}
			var extension = filterContext.RouteData.Values["ext"];
			if (extension == null || extension.ToString() != "jqgrid")
			{
				return;
			}
			var result = new ViewResult {ViewName = "QueryView"};
			var type = action.Action.Method.ReturnType.GetGenericArguments()[0];
			var url =
				VirtualPathUtility.ToAbsolute(string.Format("~/{0}/{1}", filterContext.GetControllerName(), action.ActionName));
			result.ViewData["jqGrid"] = new JqGrid()
				.Source(url)
				.AutoColumns(type);

			filterContext.Result = result;
		}

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
		}
	}
}