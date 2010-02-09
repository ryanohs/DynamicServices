namespace DynamicServices.Sakurity
{
	using System;

	public class TypeAccessExpression
	{
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

			return expression;
		}
	}
}