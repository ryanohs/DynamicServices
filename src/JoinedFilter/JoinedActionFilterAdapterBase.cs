namespace JoinedFilter
{
	using System;
	using System.Web.Mvc;

	public abstract class JoinedActionFilterAdapterBase<T> : IActionFilter, IJoinedFilter, IFilterPriority
		where T : IActionFilter
	{
		private T _Filter;

		public JoinedActionFilterAdapterBase()
		{
		}

		public JoinedActionFilterAdapterBase(T filter)
		{
			_Filter = filter;
		}

		public virtual T Filter
		{
			get { return _Filter; }
			set { _Filter = value; }
		}

		public virtual void OnActionExecuting(ActionExecutingContext filterContext)
		{
			EnsureFilterSet();
			_Filter.OnActionExecuting(filterContext);
		}

		private void EnsureFilterSet()
		{
			if (_Filter == null)
			{
				throw new NullReferenceException(string.Format("ActionFilter not injected, expecting type {0}", typeof (T)));
			}
		}

		public virtual void OnActionExecuted(ActionExecutedContext filterContext)
		{
			EnsureFilterSet();
			_Filter.OnActionExecuted(filterContext);
		}

		public abstract bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor);

		public virtual int GetOrder()
		{
			return Int32.MaxValue;
		}
	}
}