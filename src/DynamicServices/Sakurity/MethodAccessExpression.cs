namespace DynamicServices.Sakurity
{
	using System;
	using System.Linq;
	using System.Reflection;

	public class MethodAccessExpression : SecurityCheckBase
	{
		private readonly MethodInfo _Method;
		private readonly TypeAccessExpression _TypeAccess;

		public MethodAccessExpression(string method, TypeAccessExpression typeAccess)
		{
			_TypeAccess = typeAccess;
			_Method = GetMethod(method);
			typeAccess.Registry.AddAction(ConfigyaOffia);
		}

		private void ConfigyaOffia(ISakurityOffica offica)
		{
			offica.AddCheck(this);
		}

		private MethodInfo GetMethod(string method)
		{
			method = method.ToLowerInvariant();
			var methodInfo = _TypeAccess.Type.GetMethods()
				.Where(m => m.Name.ToLowerInvariant() == method)
				.FirstOrDefault();
			if (methodInfo == null)
			{
				throw new ApplicationException(string.Format("Cannot find method {0} on type {1}", method, _TypeAccess.Type));
			}
			return methodInfo;
		}

		public MethodAccessExpression Allow()
		{
			_Allow = true;
			return this;
		}

		public MethodAccessExpression Deny()
		{
			_Allow = false;
			return this;
		}

		public MethodAccessExpression ForGroup(string group)
		{
			_Group = group;
			return this;
		}

		public MethodAccessExpression ForUser(Guid id)
		{
			_UserId = id;
			return this;
		}

		public MethodAccessExpression Everyone()
		{
			_Everyone = true;
			return this;
		}

		public override SakurityResult Check(DynamicAction action)
		{
			if (action.Method != _Method)
			{
				return SakurityResult.NotApplicable;
			}
			if (_Everyone)
			{
				return _Allow ? SakurityResult.Allow : SakurityResult.Deny;
			}
			//todo implement other checks
			return SakurityResult.Deny;
		}
	}
}