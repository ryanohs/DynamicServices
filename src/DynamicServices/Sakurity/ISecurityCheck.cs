namespace DynamicServices.Sakurity
{
	public interface ISecurityCheck
	{
		int GetLevel();
		SakurityResult Check(DynamicAction action);
	}
}