namespace DynamicServices
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class EntityAction : DynamicAction
	{
		public EntityAction(MethodInfo method) : base(method)
		{
		}

		public override Type DeclaringType
		{
			get { return typeof (IDynamicRepository<>).MakeGenericType(new[] {_Method.DeclaringType}); }
		}

		public override object Invoke(object instance, IDictionary<string, object> parameters)
		{
			var getCommand = instance.GetType().GetMethod("Get");
			var entity = getCommand.Invoke(instance, new[] {parameters["id"]});
			return base.Invoke(entity,
			                   parameters.Where(p => p.Key.ToLowerInvariant() != "id").ToDictionary(x => x.Key, x => x.Value));
		}

		public override IList<DynamicParameter> GetParameters()
		{
			var parameters = base.GetParameters();
			parameters.Add(new DynamicParameter {Name = "id", Type = typeof (object)});
			return parameters;
		}
	}

	public class DynamicParameter
	{
		public string Name { get; set; }
		public Type Type { get; set; }
	}
}