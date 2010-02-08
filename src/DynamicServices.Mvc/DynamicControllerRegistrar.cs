namespace DynamicServices.Mvc
{
	using System;
	using System.Collections.Generic;

	public class DynamicControllerRegistrar
	{
		private static IDictionary<string, Type> _actions;

		static DynamicControllerRegistrar()
		{
			_actions = new Dictionary<string, Type>();
		}

		public static IDictionary<string, Type> Actions
		{
			get { return _actions; }
		}

		public static void Reset()
		{
			_actions.Clear();
		}

		public static void AddCommonActionFor<T>(string actionName)
			where T : DynamicActionDescriptor
		{
			actionName = actionName.ToLowerInvariant();
			_actions[actionName] = typeof (T);
		}
	}
}