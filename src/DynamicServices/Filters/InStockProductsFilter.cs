using System;
using System.Linq;
using DynamicServices.Model;

namespace DynamicServices.Filters
{
	public class InStockProductsFilter : IFilter<Product>
	{
		public IQueryable<Product> Execute(IQueryable<Product> source)
		{
			return source.Where(p => p.InStock);
		}
	}

	public class Products : IAggregate<Product>
	{
		public IQueryable<Product> InStock(IQueryable<Product> source)
		{
			return source.Where(p => p.InStock);
		}
	}

	public interface IAggregate<T>
	{
	}
}