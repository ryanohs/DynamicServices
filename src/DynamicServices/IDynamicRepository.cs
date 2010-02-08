using System.Linq;

namespace DynamicServices
{
	public interface IDynamicRepository
	{
	}

	public interface IDynamicRepository<T> : IDynamicRepository
	{
		T Get(object id);
		IQueryable<T> All();
		void Add(T entity);
		void Remove(T entity);
	}
}