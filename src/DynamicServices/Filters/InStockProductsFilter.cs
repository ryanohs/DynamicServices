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

	
}