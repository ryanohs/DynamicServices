using System.Linq;

namespace DynamicServices.Filters
{
	public interface IFilter<T>
	{
		IQueryable<T> Execute(IQueryable<T> source);
	}
}