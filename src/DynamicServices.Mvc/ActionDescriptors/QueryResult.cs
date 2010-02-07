namespace DynamicServices.Mvc.ActionDescriptors
{
	using System.Web.Mvc;

	public class QueryResult : ActionResult
	{
		private readonly object _Data;

		public QueryResult(object data)
		{
			_Data = data;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			//Todo: we need a pipeline for handling the output based on the extension of the request, maybe that can happen in result filters like with FubuMvc
			new JsonResult {Data = _Data}.ExecuteResult(context);
		}
	}
}