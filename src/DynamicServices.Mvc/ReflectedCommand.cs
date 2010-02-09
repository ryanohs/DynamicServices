namespace DynamicServices.Mvc
{
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using Microsoft.Practices.ServiceLocation;

	public class ReflectedCommand : DynamicActionDescriptor
	{
		private readonly IServiceLocator _Locator;
		private readonly IDynamicActionInvoker _Invoker;
		private DynamicAction _CommandAction;

		public ReflectedCommand(IServiceLocator locator, IDynamicActionInvoker invoker)
		{
			_Locator = locator;
			_Invoker = invoker;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			var serviceType = _CommandAction.DeclaringType;
			var service = _Locator.GetInstance(serviceType);
			if (service == null)
			{
				throw new Exception("Type not found.");
			}
			_CommandAction.Invoke(_Invoker, service, parameters);
			return new EmptyResult();
		}

		public void SetCommandMethod(DynamicAction commandAction)
		{
			_CommandAction = commandAction;
			_Parameters.Clear();
			AddParameters(commandAction);
		}
	}
}