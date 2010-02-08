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

			var method = this.Type.GetMethods()
				.Where(m => m.Name.ToLowerInvariant() == action)
				.FirstOrDefault();

			return method == null ? null : CreateAction(method);
		}

		public virtual DynamicAction CreateAction(MethodInfo method)
		{
			return new DynamicAction(method);
		}
	}
}