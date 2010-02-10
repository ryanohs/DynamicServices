namespace DynamicServices.Pagination
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Pipeline;
	using Sakurity;

	public class PaginationStage : IDynamicActionInvoker
	{
		private readonly IDynamicActionInvoker _Invoker;

		public PaginationStage(DomainActionInvoker invoker)
		{
			_Invoker = invoker;
		}

		public object Invoke(DynamicAction action, IDictionary<string, object> parameters)
		{
			var result = _Invoker.Invoke(action, parameters);
			var returnType = action.Method.ReturnType;
			if(!(returnType.IsGenericType && 
				returnType.GetGenericTypeDefinition() == typeof(IQueryable<>)))
			{
				return result;
			}
			result = PageResult(result);
			return result;
		}

		private object PageResult(object result)
		{
			return Utilities.ToPagedList(result);	
		}
	}
}