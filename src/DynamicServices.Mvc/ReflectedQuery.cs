namespace DynamicServices.Mvc
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Web.Mvc;
	using ActionDescriptors;
	using Microsoft.Practices.ServiceLocation;

	public class ReflectedQuery : DynamicActionDescriptor
	{
		private readonly IServiceLocator _Locator;
		private MethodInfo _QueryMethod;

		public ReflectedQuery(IServiceLocator locator)
		{
			_Locator = locator;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			var queryType = _QueryMethod.DeclaringType;
			var query = _Locator.GetInstance(queryType);
			var data = _QueryMethod.Invoke(query, parameters.Select(p => p.Value).ToArray());
			return new QueryResult(data);
		}

		public void SetQueryMethod(MethodInfo queryMethod)
		{
			_QueryMethod = queryMethod;
			AddParameters(queryMethod.GetParameters());
		}
	}
}