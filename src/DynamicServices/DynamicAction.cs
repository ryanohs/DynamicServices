namespace DynamicServices
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class DynamicAction
	{
		protected readonly DynamicType _Type;

		public DynamicAction(DynamicType type, MethodInfo methodInfo)
		{
			_Type = type;
			Method = methodInfo;
		}

		public MethodInfo Method { get; set; }

		public virtual Type Type
		{
			get { return _Type.Type; }
		}

		public virtual bool IsCommand()
		{
			return Method.ReturnType == typeof (void);
		}

		public virtual bool IsQuery()
		{
			return !IsCommand();
		}

		public virtual bool IsCollectionQuery()
		{
			return IsQuery() && Method.ReturnType.GetInterfaces().Any(i => i == typeof (IQueryable));
		}

		public virtual object Invoke(IDynamicStage invoker, IDictionary<string, object> parameters)
		{
			return invoker.Invoke(this, parameters);
		}
	}
}