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
			_Method = methodInfo;
		}

		protected MethodInfo _Method { get; set; }

		public virtual Type Type
		{
			get { return _Type.Type; }
		}

		public virtual bool IsCommand()
		{
			return _Method.ReturnType == typeof (void);
		}

		public virtual bool IsQuery()
		{
			return !IsCommand();
		}

		public virtual object Invoke(IDynamicActionInvoker invoker, object instance, IDictionary<string, object> parameters)
		{
			return invoker.Invoke(_Method, instance, parameters);
		}

		public virtual IList<DynamicParameter> GetParameters()
		{
			var parameters = from p in _Method.GetParameters()
			                 select new DynamicParameter
			                        {
			                        	Name = p.Name,
			                        	Type = p.ParameterType
			                        };
			return parameters.ToList();
		}
	}
}