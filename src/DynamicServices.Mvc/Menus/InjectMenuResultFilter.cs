namespace DynamicServices.Mvc.Menus
{
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	using MvcContrib.UI.MenuBuilder;
	using Sakurity;

	public class InjectMenuResultFilter : IResultFilter
	{
		private readonly ServicesRegistry _Services;
		private readonly ISakurityOffica _Offica;
		public string MenuKey = "MenuKey";

		public InjectMenuResultFilter(ServicesRegistry services, ISakurityOffica offica)
		{
			_Services = services;
			_Offica = offica;
		}

		public void OnResultExecuting(ResultExecutingContext filterContext)
		{
			var viewResult = filterContext.Result as ViewResultBase;
			if (viewResult == null)
			{
				return;
			}

			SetMenu(filterContext, viewResult);
		}

		private void SetMenu(ResultExecutingContext filterContext, ViewResultBase viewResult)
		{
			var menu = Menu.Begin();
			menu.SetListClass("sf-menu");
			_Services.ForEach(service => menu.Add(BuildSection(service)));
			menu.Prepare(filterContext.Controller.ControllerContext);
			viewResult.ViewData[MenuKey] = menu.RenderHtml();
		}

		private MenuList BuildSection(DynamicService service)
		{
			var items = service.GetActions()
				.Select(s => BuildMenuItem(s, service))
				.Where(s => s != null);
			var section = Menu.Items(service.Name, items.ToArray());
			// mvc contrib menu builder should allow me to controll collapsing menus with one item instead of just doing it: idiots
			if (items.Count() == 1)
			{
				section[0].Title = string.Format("{0} - {1}", service.Name, section[0].Title);
			}
			return section;
		}

		private MenuItem BuildMenuItem(DynamicAction s, DynamicService service)
		{
			if (s is EntityAction || (!s.IsCollectionQuery() && s.IsQuery())|| !_Offica.IzOk(s))
			{
				return null;
			}
			var action = s.Method.Name;
			if (s.IsCollectionQuery())
			{
				action = action + ".jqgrid";
			}

			return new MenuItem
			       {
			       	Title = s.Method.Name,
			       	ActionUrl = VirtualPathUtility.ToAbsolute(string.Format("~/{0}/{1}", service.Name, action))
			       	,
			       	AnchorClass = "parent",
			       };
		}

		public void OnResultExecuted(ResultExecutedContext filterContext)
		{
		}
	}
}