namespace DynamicServices.Pipeline
{
	public class FilterExecutor
	{
		public object Execute(object filter, object source)
		{
			return filter.GetType().GetMethod("Execute").Invoke(filter, new[] { source });
		}
	}
}