namespace DynamicServices.Mvc
{
	using System.Web.Mvc;
	using Microsoft.Practices.ServiceLocation;

	public class QueryActions : IFindAction
	{
		private readonly IServiceLocator _Locator;
		private readonly ServicesRegistry _Registry;

		public QueryActions(IServiceLocator locator, ServicesRegistry registry)
		{
			_Locator = locator;
			_Registry = registry;
		}

		public virtual ActionDescriptor FindAction(ControllerContext controllerContext,
		                                           ControllerDescriptor controllerDescriptor, string actionName)
		{
			// Todo we may want convention here.
			//if (controllerContext.HttpContext.Request.HttpMethod != Verbs.Get)
			//{
			//    return null;
			//}
			var controllerName = controllerContext.RouteData.Values["controller"].ToString().ToLowerInvariant();
			var service = _Registry.GetService(controllerName);
			if (service == null)
			{
				return null;
			}
			var queryAction = service.FindAction(actionName);
			if (queryAction == null || !queryAction.IsQuery())
			{
				return null;
			}
			var queryDescriptor = _Locator.GetInstance<ReflectedQuery>();
			queryDescriptor.SetActionName(actionName);
			queryDescriptor.SetControllerDescriptor(controllerDescriptor);
			queryDescriptor.SetQueryMethod(queryAction);
			return queryDescriptor;
		}
	}
}