namespace DynamicServices.Pagination
{
	using System.Collections.Generic;
	using System.Linq;

	public abstract class QueryableStage : IDynamicStage
	{
		protected bool ResultIsQueryable(DynamicAction action)
		{
			var returnType = action.Method.ReturnType;

			return (returnType.IsGenericType &&
			        returnType.GetGenericTypeDefinition() == typeof (IQueryable<>));
		}

		public abstract object Invoke(DynamicAction action, IDictionary<string, object> parameters);
		public abstract IList<DynamicParameter> GetParameters(DynamicAction action);
	}
}