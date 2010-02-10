namespace DynamicServices.Sakurity
{
	using System;

	public abstract class SecurityCheckBase : ISecurityCheck
	{
		protected bool _Allow;
		protected bool _Everyone;
		protected string _Group;
		private int _Level = 1;
		protected Guid _UserId = Guid.Empty;

		public virtual int GetLevel()
		{
			// ToDo use default level somehow?
			return _Level;
		}

		public abstract SakurityResult Check(DynamicAction action);
	}
}