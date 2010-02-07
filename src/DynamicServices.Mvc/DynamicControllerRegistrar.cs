namespace DynamicServices.Mvc
{
	using System;
	using System.Collections.Generic;

	public class DynamicControllerRegistrar
	{
		private static IDictionary<string, Type> _actions;
		private static IDictionary<string, Type> _commands;
		private static IDictionary<string, Type> _queries;

		static DynamicControllerRegistrar()
		{
			_actions = new Dictionary<string, Type>();
			_commands = new Dictionary<string, Type>();
			_queries = new Dictionary<string, Type>();
		}

		public static IDictionary<string, Type> Actions
		{
			get { return _actions; }
		}

		public static IDictionary<string, Type> Commands
		{
			get { return _commands; }
		}

		public static IDictionary<string, Type> Queries
		{
			get { return _queries; }
		}
	
		public static void Reset()
		{
			_actions.Clear();
			_commands.Clear();
			_queries.Clear();
		}

		public static void AddCommonActionFor<T>(string actionName)
			where T : DynamicActionDescriptor
		{
			actionName = actionName.ToLowerInvariant();
			_actions[actionName] = typeof (T);
		}

		public static void AddCommandHandler<T>(string controllerName)
		{
			controllerName = controllerName.ToLowerInvariant();
			_commands[controllerName] = typeof(T);
		}

		public static void AddQueryHandler<T>(string controllerName)
		{
			controllerName = controllerName.ToLowerInvariant();
			_queries[controllerName] = typeof(T);
		}
	}
}