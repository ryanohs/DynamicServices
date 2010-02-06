using System.Linq;

namespace Tests.Model
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