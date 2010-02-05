namespace JoinedFilter
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;

	public class JoinedFilterLocator : IFilterLocator
	{
		private IList<IJoinedFilter> JoinedFilters;

		public JoinedFilterLocator(IList<IJoinedFilter> joinedFilters)
		{
			JoinedFilters = joinedFilters;
		}

		public virtual FilterInfo FindFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var filters = new FilterInfo();

			var joinedFilters = JoinedFilters
				.Where(i => i.JoinsTo(controllerContext, actionDescriptor)).ToList();

			if (joinedFilters != null)
			{
				AddFilters(joinedFilters, filters.ActionFilters);
				AddFilters(joinedFilters, filters.ExceptionFilters);
				AddFilters(joinedFilters, filters.AuthorizationFilters);
				AddFilters(joinedFilters, filters.ResultFilters);
			}

			return filters;
		}

		private void AddFilters<T>(IEnumerable<IJoinedFilter> joinedFilters, IList<T> filters)
		{
			var orderedFilters = joinedFilters.OfType<T>()
				.OrderByDescending(f => f is IFilterPriority ? (f as IFilterPriority).GetOrder() : int.MaxValue)
				.ToList();

			orderedFilters.ForEach(filters.Add);
		}
	}
}