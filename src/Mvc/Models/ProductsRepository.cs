namespace Mvc.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DynamicServices;

	public class ProductsRepository : IDynamicRepository<Product>
	{
		private static IList<Product> _objects;

		public ProductsRepository()
		{
			_objects =
				new List<Product>(Enumerable.Range(0, 10).Select(i => new Product {Id = i, Price = 10-i, Name = "Product " + i}));
		}

		public Product Get(object id)
		{
			return _objects.SingleOrDefault(p => p.Id == Convert.ToInt32(id));
		}

		public IQueryable<Product> All()
		{
			return _objects.AsQueryable();
		}

		public void Add(Product entity)
		{
			_objects.Add(entity);
		}

		public void Remove(Product entity)
		{
			if (_objects.Contains(entity)) _objects.Remove(entity);
		}
	}
}