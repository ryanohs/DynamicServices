namespace DynamicServices.Pagination
{
	using System.Collections.Generic;
	using System.Linq;
	using Pipeline;
	using Sakurity;

	public class PaginationStage : IDynamicStage
	{
		public const string PagingCriteriaKey = "PagingCriteria";
		private readonly IDynamicStage _Invoker;

		public PaginationStage(DomainActionInvoker invoker)
		{
			_Invoker = invoker;
		}

		public object Invoke(DynamicAction action, IDictionary<string, object> parameters)
		{
			var result = _Invoker.Invoke(action,
			                             parameters.Where(p => p.Key != PagingCriteriaKey).ToDictionary(x => x.Key, x => x.Value));
			if (!ResultIsPageable(action))
			{
				return result;
			}
			var pagingCriteria =
				parameters.Where(p => p.Key == PagingCriteriaKey && p.Value.GetType() == typeof(PagingCriteria)).FirstOrDefault().
					Value as PagingCriteria;
			result = PageResult(result, pagingCriteria);
			return result;
		}

		private bool ResultIsPageable(DynamicAction action)
		{
			var returnType = action.Method.ReturnType;

			return (returnType.IsGenericType &&
			        returnType.GetGenericTypeDefinition() == typeof (IQueryable<>));
		}

		public IList<DynamicParameter> GetStageParameters(DynamicAction action)
		{
			var parameters = _Invoker.GetStageParameters(action);
			if (ResultIsPageable(action))
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