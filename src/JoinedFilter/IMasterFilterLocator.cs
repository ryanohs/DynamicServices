namespace JoinedFilter
{
	using System.Web.Mvc;

	public interface IMasterFilterLocator
	{
		void AddComposedFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor, FilterInfo filters);
	}
}