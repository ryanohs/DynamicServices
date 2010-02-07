namespace DynamicServices.Mvc
{
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class ViewWithoutModel : DynamicActionDescriptor
	{
		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			return new ViewResult {ViewName = ActionName};
		}

		public override ParameterDescriptor[] GetParameters()
		{
			return new ParameterDescriptor[0];
		}
	}
}