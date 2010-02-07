namespace DynamicServices.Mvc
{
	using System.Web.Mvc;
	using Microsoft.Practices.ServiceLocation;

	public class CommonActions : IFindAction
	{
		private readonly IServiceLocator _Locator;

		public CommonActions(IServiceLocator locator)
		{
			_Locator = locator;
		}

		public virtual ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
		{
			if (DynamicControllerRegistrar.Actions.ContainsKey(actionName))
			{
				var actionType = DynamicControllerRegistrar.Actions[actionName];
				var dynamicAction = _Locator.GetInstance(actionType) as DynamicActionDescriptor;
				if (dynamicAction != null)
				{
					dynamicAction.SetControllerDescriptor(controllerDescriptor);
					dynamicAction.SetActionName(actionName);
					return dynamicAction;
				}
			}
			return null;
		}
	}
}