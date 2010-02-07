namespace DynamicServices.Mvc
{
	using System.Linq;
	using System.Web.Mvc;
	using Microsoft.Practices.ServiceLocation;

	public class DynamicActionInvoker : ControllerActionInvoker
	{
		private readonly IServiceLocator _Locator;

		public DynamicActionInvoker(IServiceLocator locator)
		{
			_Locator = locator;
		}

		protected override ActionDescriptor FindAction(ControllerContext controllerContext,
		                                               ControllerDescriptor controllerDescriptor, string actionName)
		{
			var action = base.FindAction(controllerContext, controllerDescriptor, actionName);
			if (action != null)
			{
				return action;
			}
			
			var actionFinders = _Locator.GetAllInstances<IFindAction>();
			if (actionFinders == null)
			{
				return null;
			}

			return actionFinders
				.Select(f => f.FindAction(controllerContext, controllerDescriptor, actionName))
				.Where(d => d != null)
				.FirstOrDefault();
		}
	}
}