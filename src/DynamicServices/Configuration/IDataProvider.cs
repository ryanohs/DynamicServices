using System.Linq;

namespace DynamicServices.Configuration
{
	public interface IDataProvider
	{
		IQueryable<T> GetAll<T>();
	}
}