namespace DynamicServices
{
	using System.Collections.Generic;

	public interface IDynamicActionInvoker
	{
		object Invoke(DynamicAction action, IDictionary<string, object> parameters);
		IList<DynamicParameter> GetStageParameters(DynamicAction action);
	}
}