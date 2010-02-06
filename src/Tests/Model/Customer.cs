namespace Tests.Model
{
	public class Customer : Entity
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public double TotalDollarsSpent { get; set; }
	}
}