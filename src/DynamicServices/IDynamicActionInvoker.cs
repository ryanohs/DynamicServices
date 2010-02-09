namespace DynamicServices
{
	using System.Collections.Generic;
	using System.Reflection;

	public interface IDynamicActionInvoker
	{
		object Invoke(MethodInfo method, object instance, IDictionary<string, object> parameters);
	}
}