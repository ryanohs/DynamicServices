namespace DynamicServices
{
	using System;
	using System.Reflection;

	public class EntityType : DynamicType
	{
		public EntityType(Type type) : base(type)
		{
		}

		public override DynamicAction CreateAction(MethodInfo method)
		{
			return new EntityAction(method);
		}
	}
}