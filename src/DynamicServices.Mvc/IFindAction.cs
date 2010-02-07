namespace DynamicServices.Mvc
{
	using System.Web.Mvc;

	public interface IFindAction
	{
		ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName);
	}
}