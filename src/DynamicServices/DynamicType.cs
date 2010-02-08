namespace DynamicServices
{
	using System;
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
			action = action.ToLowerInvariant();
			// todo this should be a convention point, to allow finding a property or method first, but might not really matter all that much.
			// todo maybe by convention allow disallow property scanning?  seems like a bad thing to add
			var method = TryGetMethod(action) ?? TryGetProperty(action);

			return method == null ? null : CreateAction(method);
		}

		private MethodInfo TryGetProperty(string action)
		{
			// todo convention point for what getter/setters are prefixed with in the action name
			var isSetter = action.StartsWith("Set", StringComparison.InvariantCultureIgnoreCase);
			var isGetter = action.StartsWith("Get", StringComparison.InvariantCultureIgnoreCase);
			if((!isSetter && !isGetter) || action.Length < 4)
			{
				return null;
			}
			var property = Type.GetProperties()
				.Where(p => p.Name.ToLowerInvariant() == action.Substring(3))
				.FirstOrDefault();
			if(property == null)
			{
				return null;
			}
			return isSetter ? property.GetSetMethod() : property.GetGetMethod();
		}

		private MethodInfo TryGetMethod(string action)
		{
			return Type.GetMethods()
				.Where(m => m.Name.ToLowerInvariant() == action)
				.FirstOrDefault();
		}

		public virtual DynamicAction CreateAction(MethodInfo method)
		{
			return new DynamicAction(method);
		}
	}
}