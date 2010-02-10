namespace DynamicServices.Mvc
{
	using System.Collections.Generic;
	using System.Web.Mvc;
	using ActionDescriptors;

	public class ReflectedQuery : DynamicActionDescriptor
	{
		private readonly IDynamicStage _Invoker;
		private DynamicAction _QueryAction;

		public ReflectedQuery(IDynamicStage invoker)
		{
			_Invoker = invoker;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			var data = _QueryAction.Invoke(_Invoker, parameters);
			return new QueryResult(data);
		}

		public void SetQueryMethod(DynamicAction queryAction)
		{
			_QueryAction = queryAction;
			_Parameters.Clear();
			AddParameters(_Invoker, queryAction);
		}
	}
}