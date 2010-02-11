namespace Mvc.Models
{
	using System.Collections.Generic;
	using System.Linq;
	using DynamicServices;

	public class Location
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Employees { get; set; }
	}

	public class LocationQueries
	{
		public IQueryable<Location> All()
		{
			return Enumerable.Range(0, 100)
				.Select(i => new Location {Id = i, Employees = i%10, Name = "Location " + i})
				.ToList()
				.AsQueryable();
		}
	}
}