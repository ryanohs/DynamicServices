using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using DynamicServices.Pipeline;

namespace DynamicServices.Mvc
{
	public class DynamicActionDescriptor : ActionDescriptor
	{
		private readonly string _ActionName;
		private readonly ControllerDescriptor _ControllerDescriptor;
		private readonly QueryModelInspector _ModelInspector;

		public DynamicActionDescriptor(string actionName, ControllerDescriptor controllerDescriptor, 
			QueryModelInspector modelInspector)
		{
			_ActionName = actionName;
			_ControllerDescriptor = controllerDescriptor;
			_ModelInspector = modelInspector;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			throw new NotImplementedException();
		}

		public override ParameterDescriptor[] GetParameters()
		{
			throw new NotImplementedException();
		}

		public override string ActionName
		{
			get { return _ActionName; }
		}

		public override ControllerDescriptor ControllerDescriptor
		{
			get { return _ControllerDescriptor; }
		}
	}
}