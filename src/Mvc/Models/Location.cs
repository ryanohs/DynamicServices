namespace Mvc.Models
{
	using System.Linq;

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

		public IQueryable<Location> Others()
		{
			return Enumerable.Range(0, 100)
				.Select(i => new Location {Id = i, Employees = i%20, Name = "Loc " + i})
				.ToList()
				.AsQueryable();
		}
	}
}