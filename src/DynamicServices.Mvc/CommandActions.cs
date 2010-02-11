namespace DynamicServices.Mvc
{
	using System.Web.Mvc;
	using Microsoft.Practices.ServiceLocation;

	public class CommandActions : IFindAction
	{
		private readonly IServiceLocator _Locator;
		private readonly ServicesRegistry _Registry;

		public CommandActions(IServiceLocator locator, ServicesRegistry registry)
		{
			_Locator = locator;
			_Registry = registry;
		}

		public virtual ActionDescriptor FindAction(ControllerContext controllerContext,
		                                           ControllerDescriptor controllerDescriptor, string actionName)
		{
			// Todo we may want convention here.
			//if (controllerContext.HttpContext.Request.HttpMethod != Verbs.Post)
			//{
			//    return null;
			//}
			var controllerName = controllerContext.RouteData.Values["controller"].ToString().ToLowerInvariant();
			var service = _Registry.GetService(controllerName);
			if (service == null)
			{
				return null;
			}
			var commandAction = service.FindAction(actionName);
			if (commandAction == null || !commandAction.IsCommand())
			{
				return null;
			}
			var commandDescriptor = _Locator.GetInstance<ReflectedCommand>();
			commandDescriptor.SetActionName(actionName);
			commandDescriptor.SetControllerDescriptor(controllerDescriptor);
			commandDescriptor.SetAction(commandAction);
			return commandDescriptor;
		}
	}
}