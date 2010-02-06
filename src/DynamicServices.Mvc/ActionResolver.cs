using System.Web.Mvc;

namespace DynamicServices.Mvc
{
	public class DynamicController : Controller
	{
		public DynamicController(DynamicActionInvoker actionInvoker)
		{
			ActionInvoker = actionInvoker;
		}
	}
}