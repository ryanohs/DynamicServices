namespace DynamicServices.Mvc
{
	using System;
	using System.Linq;
	using System.Web.Mvc;
	using Microsoft.Practices.ServiceLocation;

	public class CommandActions : IFindAction
	{
		private readonly IServiceLocator _Locator;

		public CommandActions(IServiceLocator locator)
		{
			_Locator = locator;
		}

		public virtual ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
		{
			//if (controllerContext.HttpContext.Request.HttpMethod != Verbs.Post)
			//{
			//    return null;
			//}

			var controllerName = controllerContext.RouteData.Values["controller"].ToString().ToLowerInvariant();
			if (!DynamicControllerRegistrar.Commands.ContainsKey(controllerName))
			{
				return null;
			}
			var commandType = DynamicControllerRegistrar.Commands[controllerName];
			var commandName = actionName.ToLowerInvariant();
			var commandMethod = commandType.GetMethods()
				.Where(m => m.Name.ToLowerInvariant() == commandName)
				.Where(m => m.ReturnType == typeof(void))
				.FirstOrDefault();
			if (commandMethod == null)
			{
				return null;
			}
			var commandDescriptor = _Locator.GetInstance<ReflectedCommand>();
			commandDescriptor.SetActionName(actionName);
			commandDescriptor.SetControllerDescriptor(controllerDescriptor);
			commandDescriptor.SetCommandMethod(commandMethod);
			return commandDescriptor;
		}
	}
}