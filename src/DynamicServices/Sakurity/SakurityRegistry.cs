namespace DynamicServices.Sakurity
{
	using System;
	using System.Collections.Generic;

	public class SakurityRegistry
	{
		private List<Action<ISakurityOffica>> actions = new List<Action<ISakurityOffica>>();
		public int DefaultLevel { get; set; }

		public TypeAccessExpression For<T>()
		{
			return new TypeAccessExpression(typeof (T), this);
		}

		public void ConfigyaOffia(ISakurityOffica offica)
		{
			actions.ForEach(a => a(offica));
		}

		public void AddAction(Action<ISakurityOffica> action)
		{
			actions.Add(action);
		}
	}
}