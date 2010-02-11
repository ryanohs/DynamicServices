namespace DynamicServices.Pagination
{
	using System.Collections.Generic;
	using System.Linq;
	using Pipeline;
	using Sorting;

	public class PaginationStage : QueryableStage
	{
		public const string PagingCriteriaKey = "PagingCriteria";
		private readonly IDynamicStage _Invoker;

		public PaginationStage(SortingStage invoker)
		{
			_Invoker = invoker;
		}

		public override object Invoke(DynamicAction action, IDictionary<string, object> parameters)
		{
			var result = _Invoker.Invoke(action,
			                             parameters.Where(p => p.Key != PagingCriteriaKey).ToDictionary(x => x.Key, x => x.Value));
			if (!ResultIsQueryable(action))
			{
				return result;
			}
			var pagingCriteria =
				parameters.Where(p => p.Key == PagingCriteriaKey && p.Value.GetType() == typeof (PagingCriteria)).FirstOrDefault().
					Value as PagingCriteria;
			result = PageResult(result, pagingCriteria);
			return result;
		}

		public override IList<DynamicParameter> GetParameters(DynamicAction action)
		{
			var parameters = _Invoker.GetParameters(action);
			if (ResultIsQueryable(action))
			{
				parameters.Add(new DynamicParameter {Name = PagingCriteriaKey, Type = typeof (PagingCriteria)});
			}
			return parameters;
		}

		private object PageResult(object result, PagingCriteria criteria)
		{
			return Utilities.ToPagedList(result, criteria);
		}
	}
}