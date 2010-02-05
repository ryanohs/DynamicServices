namespace ReflectedAutoMap
{
	using System.Web.Mvc;

	public class ReflectedAutoMapAttribute : ActionFilterAttribute
	{
		public ReflectedAutoMapFilter ReflectedAutoMapFilter { get; set; }

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			ReflectedAutoMapFilter.OnActionExecuted(filterContext);
		}
	}
}