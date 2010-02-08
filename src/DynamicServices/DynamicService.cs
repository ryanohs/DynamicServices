namespace DynamicServices
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// This is the registration of a dynamic service.  A service aggregates operations on a set of domain types.
	/// </summary>
	public class DynamicService
	{
		public DynamicService(string name)
		{
			Name = name;
			Types = new List<DynamicType>();
		}

		public string Name { get; protected set; }

		public IList<DynamicType> Types { get; protected set; }

		public DynamicService For<T>()
		{
			Types.Add(new DynamicType(typeof (T)));
			return this;
		}

		public DynamicService Entity<T>()
		{
			Types.Add(new EntityType(typeof (T)));
			return this;
		}

		public DynamicAction FindAction(string action)
		{
			return Types
				.Select(t => t.FindAction(action))
				.Where(a => a != null)
				.FirstOrDefault();
		}

	}
}