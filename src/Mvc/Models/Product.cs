namespace Mvc.Models
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public void SetPrice(decimal price)
		{
			Price = price;
		}
	}
}