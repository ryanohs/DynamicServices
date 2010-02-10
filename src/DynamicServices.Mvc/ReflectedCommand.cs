namespace DynamicServices.Mvc
{
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class ReflectedCommand : DynamicActionDescriptor
	{
		private readonly IDynamicStage _Invoker;
		private DynamicAction _CommandAction;

		public ReflectedCommand(IDynamicStage invoker)
		{
			_Invoker = invoker;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			_CommandAction.Invoke(_Invoker, parameters);
			return new EmptyResult();
		}

		public void SetCommandMethod(DynamicAction commandAction)
		{
			_CommandAction = commandAction;
			_Parameters.Clear();
			AddParameters(_Invoker, commandAction);
		}
	}
}