namespace DynamicServices.Mvc
{
	using System.Web.Mvc;

	public class ReflectedCommand : ReflectedDynamicActionDescriptor
	{
		public ReflectedCommand(IDynamicStage stage) : base(stage)
		{
		}

		protected override object GetResult(object data)
		{
			return new EmptyResult();
		}
	}
}