using System.Linq;

namespace Tests.Repositories
{
	public interface IRepository<T>
	{
		IQueryable<T> All { get; }
	}
}