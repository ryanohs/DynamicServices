namespace JoinedFilter
{
	using System.Web.Mvc;

	public interface IJoinedFilter
	{
		bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
	}
}