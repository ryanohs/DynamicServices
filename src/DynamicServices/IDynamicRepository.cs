namespace DynamicServices
{
	public interface IDynamicRepository<T>
	{
		T Get(object id);
		void Add(T entity);
		void Remove(T entity);
	}
}