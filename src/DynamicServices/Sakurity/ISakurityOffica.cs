namespace DynamicServices.Sakurity
{
	public interface ISakurityOffica
	{
		// Todo maybe another over load for a MethodInfo only, maybe it's a bad idea to use DynamicAction here as we are configuring security solely on Reflection of the domain and not with regards to DynamicActions.
		void SakuritySakurity(DynamicAction action);
		// Todo add parameter for user context so we can check roles and user id and what not
		void AddCheck(ISecurityCheck check);
	}
}