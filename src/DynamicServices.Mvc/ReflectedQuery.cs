namespace DynamicServices.Mvc
{
	using ActionDescriptors;

	public class ReflectedQuery : ReflectedDynamicActionDescriptor
	{
		public ReflectedQuery(IDynamicStage stage) : base(stage)
		{
		}

		protected override object GetResult(object data)
		{
			return new QueryResult(data);
		}
	}
}