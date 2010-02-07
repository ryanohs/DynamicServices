namespace DynamicServices.Mvc
{
	using System.Linq;
	using System.Web.Mvc;
	using Microsoft.Practices.ServiceLocation;

	public class QueryActions : IFindAction
	{
		private readonly IServiceLocator _Locator;

		public QueryActions(IServiceLocator locator)
		{
			_Locator = locator;
		}

		public virtual ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
		{
			var controllerName = controllerDescriptor.ControllerName.ToLowerInvariant();
			if (!DynamicControllerRegistrar.Queries.ContainsKey(controllerName))
			{
				return null;
			}
			var queryType = DynamicControllerRegistrar.Queries[controllerName];
			var queryName = actionName.ToLowerInvariant();
			var queryMethod = queryType.GetMethods()
				.Where(m => m.Name.ToLowerInvariant() == queryName)
				.FirstOrDefault();
			if (queryMethod == null)
			{
				return null;
			}
			var queryDescriptor = _Locator.GetInstance<ReflectedQuery>();
			queryDescriptor.SetActionName(actionName);
			queryDescriptor.SetControllerDescriptor(controllerDescriptor);
			queryDescriptor.SetQueryMethod(queryMethod);
			return queryDescriptor;
		}
	}
}