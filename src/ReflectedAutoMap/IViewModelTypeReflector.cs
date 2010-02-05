namespace ReflectedAutoMap
{
	using System;
	using System.Web.Mvc;

	public interface IViewModelTypeReflector
	{
		Type GetDestinationModelType(ActionExecutedContext filterContext);
	}
}