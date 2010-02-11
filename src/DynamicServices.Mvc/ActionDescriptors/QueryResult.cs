namespace DynamicServices.Mvc.ActionDescriptors
{
	using System.Collections;
	using System.Web.Mvc;

	public class QueryResult : ActionResult
	{
		public QueryResult(object data)
		{
			Data = data;
		}

		public object Data { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			//Todo: we need a pipeline for handling the output based on the extension of the request, maybe that can happen in result filters like with FubuMvc

			var json = new
			           {
			           	rows = Data as IEnumerable,
			           	page = Data.GetType().GetProperty("PageNumber").GetValue(Data, null),
			           	records = Data.GetType().GetProperty("TotalItemCount").GetValue(Data, null),
			           	total = Data.GetType().GetProperty("PageCount").GetValue(Data, null)
			           };

			new JsonResult {Data = json}.ExecuteResult(context);
		}
	}
}