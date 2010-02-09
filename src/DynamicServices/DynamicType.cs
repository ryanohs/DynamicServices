namespace DynamicServices
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class DynamicType
	{
		public DynamicType(Type type)
		{
			Type = type;
		}

		public Type Type { get; protected set; }

		public virtual DynamicAction FindAction(string action)
		{
			// todo this should be a convention point, to allow finding a property or method first, but might not really matter all that much.
			// todo maybe by convention allow disallow property scanning?  seems like a bad thing to add
			var method = TryGetMethod(action) ?? TryGetProperty(action);

			return method == null ? null : CreateAction(method);
		}

		private MethodInfo TryGetProperty(string action)
		{
			action = action.ToLowerInvariant();
			// todo convention point for what getter/setters are prefixed with in the action name
			var isSetter = action.StartsWith("set", StringComparison.InvariantCultureIgnoreCase);
			var isGetter = action.StartsWith("get", StringComparison.InvariantCultureIgnoreCase);
			if ((!isSetter && !isGetter) || action.Length < 4)
			{
				return null;
			}

			return TryGetMethod(action.Substring(0, 3) + "_" + action.Substring(3));
		}

		private MethodInfo TryGetMethod(string action)
		{
			action = action.ToLowerInvariant();
			return GetMethods()
				.Where(m => m.Name.ToLowerInvariant() == action)
				.FirstOrDefault();
		}

		public virtual DynamicAction CreateAction(MethodInfo method)
		{
			return new DynamicAction(this, method);
		}

		public IList<DynamicAction> GetActions()
		{
			var methods = GetMethods();
			return methods.Select(m => CreateAction(m)).ToList();
		}

		private List<MethodInfo> GetMethods()
		{
			return Type.GetMethods()
				.Where(m => m.IsPublic)
				.ToList();
		}
	}
}