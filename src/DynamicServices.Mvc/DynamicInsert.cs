using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicServices.Mvc
{
	public class DynamicInsert : DynamicActionDescriptor
	{
		private readonly ServicesRegistry _Registry;

		public DynamicInsert(ServicesRegistry registry)
		{
			_Registry = registry;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			var controllerName = controllerContext.RouteData.Values["controller"].ToString().ToLowerInvariant();
			var service = _Registry.GetService(controllerName);
			var type = service.Types[0].Type;
			var emptyEntity = Activator.CreateInstance(type);
			var result = new ViewResult { ViewName = "EditorView" };
			result.ViewData.Model = emptyEntity;
			result.ViewData["properties"] = type.GetProperties();
			return result;
		}

		public override ParameterDescriptor[] GetParameters()
		{
			return new ParameterDescriptor[0];
		}
	}
}