namespace DynamicServices.Sakurity
{
	using System;

	public class TypeAccessExpression : SecurityCheckBase
	{
		private bool _OnAllCommands;
		private bool _OnAllQueries;

		public TypeAccessExpression(Type type, SakurityRegistry registry)
		{
			Type = type;
			Registry = registry;
		}

		public SakurityRegistry Registry { get; protected set; }

		public Type Type { get; protected set; }

		public MethodAccessExpression Allow(string method)
		{
			var expression = new MethodAccessExpression(method, this);
			expression.Allow();
			return expression;
		}

		public MethodAccessExpression Deny(string method)
		{
			var expression = new MethodAccessExpression(method, this);
			expression.Deny();
			return expression;
		}

		public TypeAccessExpression Allow()
		{
			var expression = new TypeAccessExpression(Type, Registry) {_Allow = true};
			Registry.AddAction(expression.ConfigyaOffia);
			return expression;
		}

		private void ConfigyaOffia(ISakurityOffica offica)
		{
			offica.AddCheck(this);
		}

		public TypeAccessExpression Deny()
		{
			var expression = new TypeAccessExpression(Type, Registry) {_Allow = false};
			Registry.AddAction(expression.ConfigyaOffia);
			return expression;
		}

		public TypeAccessExpression OnAll()
		{
			_OnAllCommands = true;
			_OnAllQueries = true;
			return this;
		}

		public TypeAccessExpression OnAllCommands()
		{
			_OnAllCommands = true;
			return this;
		}

		public TypeAccessExpression OnAllQueries()
		{
			_OnAllQueries = true;
			return this;
		}

		public override SakurityResult Check(DynamicAction action)
		{
			if (action.Method.ReflectedType != Type)
			{
				return SakurityResult.NotApplicable;
			}

			if(action.IsCommand() && _OnAllCommands)
			{
				return _Allow && _Everyone ? SakurityResult.Allow : SakurityResult.Deny;
			}
			if (action.IsQuery() && _OnAllQueries)
			{
				return _Allow && _Everyone ? SakurityResult.Allow : SakurityResult.Deny;
			}

			return SakurityResult.NotApplicable;
		}

		public TypeAccessExpression Everyone()
		{
			_Everyone = true;
			return this;
		}
	}
}