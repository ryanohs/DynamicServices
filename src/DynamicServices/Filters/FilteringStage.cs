namespace DynamicServices.Filters
{
	using System.Linq;
	using System.Linq.Dynamic;
	using Pagination;
	using Sakurity;

	public class FilteringStage : QueryableStage<FilteringCriteria>
	{
		public FilteringStage(DomainInvoker invoker) : base(invoker)
		{
		}

		protected override object AlterResult(IQueryable result, FilteringCriteria parameter)
		{
			if (result == null || string.IsNullOrEmpty(parameter.Filter))
			{
				return result;
			}

			try
			{
				result = result.Where(parameter.Filter, null);
			}
			catch (ParseException exception)
			{
				// Todo how to handle an invalid filter, probably need to throw a dynamic services error?
			}

			return result;
		}

		protected override string GetParameterKey()
		{
			return "FilteringCriteria";
		}
	}
}