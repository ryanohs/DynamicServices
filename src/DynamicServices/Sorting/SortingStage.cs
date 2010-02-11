namespace DynamicServices.Sorting
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Dynamic;
	using Pagination;
	using Sakurity;

	public class SortingStage : QueryableStage
	{
		public const string SortingCriteriaKey = "SortingCriteria";
		private readonly IDynamicStage _Invoker;

		public SortingStage(DomainInvoker invoker)
		{
			_Invoker = invoker;
		}

		public override object Invoke(DynamicAction action, IDictionary<string, object> parameters)
		{
			var result = _Invoker.Invoke(action,
			                             parameters.Where(p => p.Key != SortingCriteriaKey).ToDictionary(x => x.Key, x => x.Value));
			if (!ResultIsQueryable(action))
			{
				return result;
			}
			var sortingCriteria =
				parameters.Where(p => p.Key == SortingCriteriaKey && p.Value.GetType() == typeof (SortingCriteria)).FirstOrDefault()
					.
					Value as SortingCriteria;
			result = SortResult(result, sortingCriteria);
			return result;
		}

		private object SortResult(object result, SortingCriteria criteria)
		{
			if (string.IsNullOrEmpty(criteria.Sort))
			{
				return result;
			}
			var queryable = result as IQueryable;

			try
			{
				result = queryable.OrderBy(criteria.Sort, null);
			}
			catch (ParseException exception)
			{
				// Todo how to handle an invalid sort, probably need to throw a dynamic services error?
			}

			return result;
		}

		public override IList<DynamicParameter> GetParameters(DynamicAction action)
		{
			var parameters = _Invoker.GetParameters(action);
			if (ResultIsQueryable(action))
			{
				parameters.Add(new DynamicParameter {Name = SortingCriteriaKey, Type = typeof (SortingCriteria)});
			}
			return parameters;
		}
	}
}