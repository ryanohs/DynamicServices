namespace DynamicServices.Mvc
{
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using ActionDescriptors;
	using Microsoft.Practices.ServiceLocation;

	public class ReflectedQuery : DynamicActionDescriptor
	{
		private readonly IServiceLocator _Locator;
		private readonly IDynamicActionInvoker _Invoker;
		private DynamicAction _QueryAction;

		public ReflectedQuery(IServiceLocator locator, IDynamicActionInvoker invoker)
		{
			_Locator = locator;
			_Invoker = invoker;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			var serviceType = _QueryAction.DeclaringType;
			var service = _Locator.GetInstance(serviceType);
			if (service == null)
			{
				throw new Exception("Type not found.");
			}
			var data = _QueryAction.Invoke(_Invoker, service, parameters);
			return new QueryResult(data);
		}

		public void SetQueryMethod(DynamicAction queryAction)
		{
			_QueryAction = queryAction;
			_Parameters.Clear();
			AddParameters(queryAction);
		}
	}
}