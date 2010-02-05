namespace ReflectedAutoMap.JoinedFilter
{
	using global::JoinedFilter;

	/// <summary>
	/// Naming format for joined filter + action filter implementations, JoinPoint_Action
	/// </summary>
	public class OnViewResult_IfModelDoesNotMatchViewModelThenMapWithAutoMapper :
		JoinedViewResultActionFilterBase<ReflectedAutoMapFilter>
	{
		public override int GetOrder()
		{
			return 1;
		}
	}
}