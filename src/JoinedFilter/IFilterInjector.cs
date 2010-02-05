namespace JoinedFilter
{
	using System.Web.Mvc;

	public interface IFilterInjector
	{
		void BuildUp(FilterInfo filters);
	}
}