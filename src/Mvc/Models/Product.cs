namespace Mvc.Models
{
	using System;

	public class Products
	{
		public void ThrowException()
		{
			throw new Exception();
		}
	}
	
	public class Product
	{
		public int Id { get; set; }
	}
}