namespace DynamicServices
{
	using System.Reflection;

	public class EntityAction : DynamicAction
	{
		public EntityAction(DynamicType type, MethodInfo method) : base(type, method)
		{
		}
	}
}