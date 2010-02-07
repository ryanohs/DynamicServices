namespace DynamicServices.Mvc.ActionDescriptors
{
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class Query : DynamicActionDescriptor
	{
		private const string _QueryParameters = "queryParameters";
		private readonly IDynamicQuery _DynamicQuery;

		public Query(IDynamicQuery dynamicQuery)
		{
			_DynamicQuery = dynamicQuery;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			var queryParameters = parameters[_QueryParameters] as QueryParameters;
			var data = _DynamicQuery.GetData(controllerContext, queryParameters);
			return new QueryResult(data);
		}

		public override ParameterDescriptor[] GetParameters()
		{
			var queryParameters = new DynamicParameterDescriptor(this, _QueryParameters, typeof (QueryParameters));
			return new ParameterDescriptor[] {queryParameters};
		}
	}
}