namespace JoinedFilter
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Web.Mvc;

	public abstract class ViewModelInjectFilter : IActionFilter, IFilterPriority
	{
		public virtual void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		public virtual void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var viewResult = filterContext.Result as ViewResultBase;
			if (viewResult == null || viewResult.ViewData.Model == null)
			{
				return;
			}
			var model = viewResult.ViewData.Model;
			var property = model.GetType().GetProperties().FirstOrDefault(InjectProperty());
			if (property == null)
			{
				return;
			}
			var value = property.GetValue(model, null);
			if (value == null)
			{
				property.SetValue(model, WithValue(), null);
			}
		}

		protected abstract Func<PropertyInfo, bool> InjectProperty();
		protected abstract object WithValue();

		public virtual int GetOrder()
		{
			return Int32.MaxValue;
		}
	}
}