namespace Mvc.Models
{
	using System;
	using DynamicServices;

	public class ProductRepository : IDynamicRepository<Product>
	{
		public Product Get(object id)
		{
			return new Product();
		}

		public void Add(Product entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(Product entity)
		{
			throw new NotImplementedException();
		}
	}
}