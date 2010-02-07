namespace DynamicServices.Mvc
{
	using System;
	using System.Web.Mvc;

	public class DynamicQuery : IDynamicQuery
	{
		public object GetData(ControllerContext context, QueryParameters parameters)
		{
			throw new NotImplementedException();
		}
	}
}