namespace DynamicServices.Sakurity
{
	using System.Reflection;

	public interface ISakurityOffica
	{
		void SakuritySakurity(MethodInfo method);
		// Todo add parameter for user context so we can check roles and user id and what not
		void AddCheck(ISecurityCheck check);
	}
}