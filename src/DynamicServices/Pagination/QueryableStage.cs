namespace DynamicServices.Pagination
{
	using System.Collections.Generic;
	using System.Linq;

	public abstract class QueryableStage<TParameter> : IDynamicStage
		where TParameter : class
	{
		protected readonly IDynamicStage _Invoker;

		protected QueryableStage(IDynamicStage invoker)
		{
			_Invoker = invoker;
		}

		protected bool ResultIsQueryable(DynamicAction action)
		{
			var returnType = action.Method.ReturnType;

			return (returnType.IsGenericType &&
			        returnType.GetGenericTypeDefinition() == typeof (IQueryable<>));
		}

		public virtual object Invoke(DynamicAction action, IDictionary<string, object> parameters)
		{
			var result = _Invoker.Invoke(action,
			                             parameters.Where(p => p.Key != GetParameterKey()).ToDictionary(x => x.Key,
			                                                                                            x => x.Value));
			if (!ResultIsQueryable(action))
			{
				return result;
			}
			var parameter =
				parameters.Where(p => p.Key == GetParameterKey() && p.Value.GetType() == typeof (TParameter)).
					FirstOrDefault()
					.Value as TParameter;
			result = AlterResult(result as IQueryable, parameter);
			return result;
		}

		protected abstract object AlterResult(IQueryable result, TParameter parameter);

		public virtual IList<DynamicParameter> GetParameters(DynamicAction action)
		{
			var parameters = _Invoker.GetParameters(action);
			if (ResultIsQueryable(action))
			{
				parameters.Add(new DynamicParameter {Name = GetParameterKey(), Type = typeof (TParameter)});
			}
			return parameters;
		}

		protected abstract string GetParameterKey();
	}
}