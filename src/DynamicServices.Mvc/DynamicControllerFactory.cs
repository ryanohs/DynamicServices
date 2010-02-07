namespace DynamicServices.Mvc
{
	using System;
	using System.Web.Mvc;
	using Microsoft.Practices.ServiceLocation;

	public class DynamicControllerFactory : DefaultControllerFactory
	{
		private readonly IServiceLocator _Locator;

		public DynamicControllerFactory(IServiceLocator locator)
		{
			_Locator = locator;
		}

		protected override Type GetControllerType(string controllerName)
		{
			var controllerType = base.GetControllerType(controllerName);
			return controllerType ?? typeof (DynamicController);
		}

		protected override IController GetControllerInstance(Type controllerType)
		{
			var controller = base.GetControllerInstance(controllerType) as Controller;

			var actionInvoker = _Locator.GetInstance<IActionInvoker>();
			if (actionInvoker != null)
			{
				controller.ActionInvoker = actionInvoker;
			}

			return controller;
		}
	}
}