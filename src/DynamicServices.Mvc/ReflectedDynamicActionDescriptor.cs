namespace DynamicServices.Mvc
{
	using System.Collections.Generic;
	using System.Web.Mvc;

	public abstract class ReflectedDynamicActionDescriptor : DynamicActionDescriptor
	{
		protected readonly IDynamicStage Stage;
		public DynamicAction Action { get; set; }

		public ReflectedDynamicActionDescriptor(IDynamicStage stage)
		{
			Stage = stage;
		}

		public void SetAction(DynamicAction action)
		{
			Action = action;
			Parameters.Clear();
			AddParameters(Stage, action);
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			var data = Action.Invoke(Stage, parameters);
			return GetResult(data);
		}

		protected abstract object GetResult(object data);
	}
}