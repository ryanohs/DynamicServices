namespace ReflectedAutoMap
{
	using System.Web.Mvc;

	public class ReflectedAutoMapFilter : IActionFilter
	{
		public IReflectedAutoMapper ReflectedAutoMapper { get; set; }

		public ReflectedAutoMapFilter(IReflectedAutoMapper reflectedAutoMapper)
		{
			ReflectedAutoMapper = reflectedAutoMapper;
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			ReflectedAutoMapper.TryMapSourceToDestination(filterContext);
		}
	}
}