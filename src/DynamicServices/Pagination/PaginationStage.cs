namespace DynamicServices.Pagination
{
	using System.Linq;
	using Pipeline;
	using Sorting;

	public class PaginationStage : QueryableStage<PagingCriteria>
	{
		public PaginationStage(SortingStage invoker) : base(invoker)
		{
		}

		protected override object AlterResult(IQueryable result, PagingCriteria parameter)
		{
			return Utilities.ToPagedList(result, parameter);
		}

		protected override string GetParameterKey()
		{
			return "PagingCriteria";
		}
	}
}