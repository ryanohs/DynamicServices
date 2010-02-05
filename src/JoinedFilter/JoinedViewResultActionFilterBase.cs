namespace JoinedFilter
{
	using System;
	using System.Web.Mvc;

	public class JoinedViewResultActionFilterBase<T> : JoinedActionFilterAdapterBase<T>
		where T : IActionFilter
	{
		private const bool JoinToNonReflectedActions = true;

		public override bool JoinsTo(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			if (!(actionDescriptor is ReflectedActionDescriptor))
			{
				return JoinToNonReflectedActions;
			}

			var returnType = (actionDescriptor as ReflectedActionDescriptor).MethodInfo.ReturnType;

			return JoinToViewResultBaseTypes(returnType)
			       || JoinToTypesThatCanHoldViewResultBase(returnType);
		}

		private static bool JoinToTypesThatCanHoldViewResultBase(Type returnType)
		{
			return returnType.IsAssignableFrom(typeof (ViewResultBase));
		}

		private static bool JoinToViewResultBaseTypes(Type returnType)
		{
			return typeof (ViewResultBase).IsAssignableFrom(returnType);
		}
	}
}