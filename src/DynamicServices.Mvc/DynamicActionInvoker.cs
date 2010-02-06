using System.Web.Mvc;
using DynamicServices.Pipeline;

namespace DynamicServices.Mvc
{
	public class DynamicActionInvoker : ControllerActionInvoker
	{
		private readonly QueryModelInspector _ModelInspector;

		public DynamicActionInvoker(QueryModelInspector modelInspector)
		{
			_ModelInspector = modelInspector;
		}

		protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
		{
			var action = base.FindAction(controllerContext, controllerDescriptor, actionName);
			
			if(action == null)
			{
				// TODO If ActionName + ControllerName - "Controller" = any filter/query names
				// Use that query
				// How exactly is this going to work? Func<>?
				action = new DynamicActionDescriptor(actionName, controllerDescriptor, _ModelInspector);

				// TODO If ActionName - ControllerName + "Controller" = name of a boolean property on the target type
				// Use the dynamic boolean filter

				// TODO Look for CUD commands

				// TODO Look for any other matching commands
			}

			return action;
		}
	}
}