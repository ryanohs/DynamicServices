using System.Linq;
using DynamicServices.Model;

namespace DynamicServices.Filters
{
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