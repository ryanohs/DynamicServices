using System;
using System.Linq;
using DynamicServices.Filters;
using Tests.Model;

namespace Tests.Filters
{
	public class InStockProductsFilter : IFilter<Product>
	{
		public IQueryable<Product> Execute(IQueryable<Product> source)
		{
			return source.Where(p => p.InStock);
		}
	}
}