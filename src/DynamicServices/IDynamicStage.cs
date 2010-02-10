namespace DynamicServices
{
	using System.Collections.Generic;

	public interface IDynamicStage
	{
		object Invoke(DynamicAction action, IDictionary<string, object> parameters);
		IList<DynamicParameter> GetStageParameters(DynamicAction action);
	}
}