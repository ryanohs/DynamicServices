namespace DynamicServices.Sorting
{
	using System.Linq;
	using System.Linq.Dynamic;
	using Filters;
	using Pagination;

	public class SortingStage : QueryableStage<SortingCriteria>
	{
		public SortingStage(FilteringStage invoker) : base(invoker)
		{
		}

		protected override object AlterResult(IQueryable result, SortingCriteria criteria)
		{
			if (result == null || string.IsNullOrEmpty(criteria.Sort))
			{
				return result;
			}

			try
			{
				result = result.OrderBy(criteria.Sort, null);
			}
			catch (ParseException exception)
			{
				// Todo how to handle an invalid sort, probably need to throw a dynamic services error?
			}

			return result;
		}

		protected override string GetParameterKey()
		{
			return "SortingCriteria";
		}
	}
}