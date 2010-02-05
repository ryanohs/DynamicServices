namespace JoinedFilter
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;

	public class MasterFilterLocator : IMasterFilterLocator
	{
		public IList<IFilterLocator> FilterLocators { get; set; }

		public MasterFilterLocator(IList<IFilterLocator> fitlerLocators)
		{
			FilterLocators = fitlerLocators ?? new List<IFilterLocator>();
		}

		public virtual void AddComposedFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor,
		                               FilterInfo filters)
		{
			var foundFilters = FilterLocators.Select(f => f.FindFilters(controllerContext, actionDescriptor));

			foundFilters.ForEach(f => AddFilters(filters, f));
		}

		protected void AddFilters(FilterInfo filters, FilterInfo mergeFilters)
		{
			mergeFilters.ActionFilters.ForEach(filters.ActionFilters.Add);
			mergeFilters.ExceptionFilters.ForEach(filters.ExceptionFilters.Add);
			mergeFilters.AuthorizationFilters.ForEach(filters.AuthorizationFilters.Add);
			mergeFilters.ResultFilters.ForEach(filters.ResultFilters.Add);
		}
	}
}