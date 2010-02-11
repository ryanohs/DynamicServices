using System.Web.Mvc;

namespace DynamicServices.Mvc.Scaffolding.JqGrid
{
	public static class JqGridExtensions
	{
		public static IJqGridBuilder JqGrid(this HtmlHelper helper)
		{
			return new JqGrid().Helper(helper);
		}
	}
}