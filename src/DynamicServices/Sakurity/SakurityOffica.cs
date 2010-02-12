namespace DynamicServices.Sakurity
{
	using System.Collections.Generic;
	using System.Linq;

	public class SakurityOffica : ISakurityOffica
	{
		private readonly IList<ISecurityCheck> _Checks = new List<ISecurityCheck>();

		public void SakuritySakurity(DynamicAction action)
		{
			if(IzOk(action))
			{
				return;
			}
			// Todo this might need to come from ISecurityCheck, format the exception message to be more or less helpful depending on what build type we have?
			throw new ThisDudeNeedTaGo("Access not allowed");
		}

		public bool IzOk(DynamicAction action)
		{
			var result =
				_Checks.OrderBy(c => c.GetLevel()).Select(c => c.Check(action)).Where(r => r != SakurityResult.NotApplicable)
					.FirstOrDefault();
			return result == SakurityResult.Allow;
		}

		public void AddCheck(ISecurityCheck check)
		{
			_Checks.Add(check);
		}
	}
}