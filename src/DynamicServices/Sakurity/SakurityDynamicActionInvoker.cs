namespace DynamicServices.Sakurity
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class SakurityDynamicActionInvoker : IDynamicActionInvoker
	{
		private readonly ISakurityOffica _Offica;

		public SakurityDynamicActionInvoker(ISakurityOffica offica)
		{
			_Offica = offica;
		}

		public object Invoke(MethodInfo method, object instance, IDictionary<string, object> parameters)
		{
			// Todo This needs to tap into a pipeline that we can configure at run time... Something like a DynamicActionPipelineRegistry where we can pick the components to use and what order to apply them.  Maybe even conditional pipelines based on convention but overriden with configuration.  The pipeline for queries could split based on return type to do things with collections versus scalar results.  The pipeline for commands would be more straight forward but might have "mapping" constructs to.
			_Offica.SakuritySakurity(method);
			return method.Invoke(instance, parameters.Select(p => p.Value).OfType<object>().ToArray());
		}
	}
}