namespace DynamicServices.Sakurity
{
	using System.Reflection;

	public interface ISecurityCheck
	{
		int GetLevel();
		SakurityResult Check(MethodInfo methodInfo);
	}
}