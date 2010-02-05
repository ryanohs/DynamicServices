namespace JoinedFilter
{
	using System.Web.Mvc;

	public class LocatorActionInvoker : ControllerActionInvoker
	{
		public IFilterInjector FilterInjector { get; set; }

		public IMasterFilterLocator MasterFilterLocator { get; set; }

		public LocatorActionInvoker(IFilterInjector filterInjector, IMasterFilterLocator masterFilterLocator)
		{
			MasterFilterLocator = masterFilterLocator;
			FilterInjector = filterInjector;
		}

		protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var filters = base.GetFilters(controllerContext, actionDescriptor);

			FilterInjector.BuildUp(filters);

			MasterFilterLocator.AddComposedFilters(controllerContext, actionDescriptor, filters);

			return filters;
		}
	}
}