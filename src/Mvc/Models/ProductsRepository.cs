using System.Collections.Generic;
using System.Linq;

namespace Mvc.Models
{
	using System;
	using DynamicServices;

	public class ProductsRepository : IDynamicRepository<Product>
	{
		private static IList<Product> _objects;

		public ProductsRepository()
		{
			_objects = new List<Product>(Enumerable.Range(0, 10).Select(i => new Product() {Id = i}));
		}
	
		public Product Get(int id)
		{
			return _objects.SingleOrDefault(p => p.Id == id);
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
			if(_objects.Contains(entity)) _objects.Remove(entity);
		}
	}
}