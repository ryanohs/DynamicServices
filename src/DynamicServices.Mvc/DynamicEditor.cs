using System.Collections.Generic;
using System.Web.Mvc;
using HtmlTags;

namespace DynamicServices.Mvc
{
	public class DynamicEditor : DynamicActionDescriptor
	{
		private readonly IDynamicQuery _DynamicQuery;

		public DynamicEditor(IDynamicQuery dynamicQuery)
		{
			_DynamicQuery = dynamicQuery;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			// TODO This probably needs to be a composite action; new concept?
			var data = _DynamicQuery.GetData(controllerContext, null);
			var result = new ViewResult { ViewName = "EditorView" };
			result.ViewData["properties"] = null;
			// TODO
			return result;
		}

		public override ParameterDescriptor[] GetParameters()
		{
			return new ParameterDescriptor[0];
		}
	}
}