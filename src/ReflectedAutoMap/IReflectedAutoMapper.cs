namespace ReflectedAutoMap
{
	using System.Web.Mvc;

	public interface IReflectedAutoMapper
	{
		void TryMapSourceToDestination(ActionExecutedContext filterContext);
	}
}