namespace ReflectedAutoMap
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Web.Compilation;
	using System.Web.Mvc;

	public class ViewModelTypeReflector : IViewModelTypeReflector
	{
		public Type GetDestinationModelType(ActionExecutedContext filterContext)
		{
			var view = GetView(filterContext);
			if (view == null)
			{
				return null;
			}

			if (view is WebFormView)
			{
				return GetWebFormViewModelType(view as WebFormView);
			}

			var modelType = GetModelType(view);

			TryDispose(view);

			return modelType;
		}

		private static void TryDispose(object view)
		{
			if (view is IDisposable)
			{
				(view as IDisposable).Dispose();
			}
		}

		private static Type GetWebFormViewModelType(WebFormView view)
		{
			var viewPage = BuildManager.CreateInstanceFromVirtualPath(view.ViewPath, typeof (object));
			if (viewPage == null)
			{
				return null;
			}

			var modelType = GetModelType(viewPage);

			TryDispose(viewPage);

			return modelType;
		}

		private static Type GetModelType(object view)
		{
			const string modelPropertyName = "Model";
			var modelMembers = view.GetType().GetMember(modelPropertyName);
			if (modelMembers == null)
			{
				return null;
			}
			var model = modelMembers.OfType<PropertyInfo>().FirstOrDefault(p => p.PropertyType != typeof (object));
			if (model == null)
			{
				return null;
			}
			return model.PropertyType;
		}

		private static IView GetView(ActionExecutedContext filterContext)
		{
			if (!(filterContext.Result is ViewResultBase))
			{
				return null;
			}
			var viewResultBase = filterContext.Result as ViewResultBase;
			var viewName = GetViewName(filterContext, viewResultBase);

			ViewEngineResult result = null;
			if (viewResultBase is ViewResult)
			{
				result = viewResultBase.ViewEngineCollection.FindView(filterContext.Controller.ControllerContext,
				                                                      viewName,
				                                                      (viewResultBase as ViewResult).MasterName);
			}
			else if (filterContext.Result is PartialViewResult)
			{
				result = viewResultBase.ViewEngineCollection.FindPartialView(filterContext.Controller.ControllerContext,
				                                                             viewName);
			}

			if (result == null)
			{
				return null;
			}

			return result.View;
		}

		private static string GetViewName(ActionExecutedContext filterContext, ViewResultBase viewResultBase)
		{
			var viewName = viewResultBase.ViewName;
			if (string.IsNullOrEmpty(viewName)) viewName = filterContext.RouteData.GetRequiredString("action");
			return viewName;
		}
	}
}