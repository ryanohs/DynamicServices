namespace DynamicServices
{
	using System.Collections.Generic;
	using System.Reflection;

	public class EntityAction : DynamicAction
	{
		public EntityAction(DynamicType type, MethodInfo method) : base(type, method)
		{
		}

		public override IList<DynamicParameter> GetParameters()
		{
			// Todo move the reflection of parameters into the pipeline so each stage can add it's own requirements, ie: the id requirement should come from a stage in the pipeline that takes an EntityAction and queries a repository for the entity.
			var parameters = base.GetParameters();
			parameters.Add(new DynamicParameter {Name = "id", Type = typeof (object)});
			return parameters;
		}
	}
}