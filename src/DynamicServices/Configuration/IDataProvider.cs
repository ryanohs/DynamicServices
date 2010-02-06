using System;
using System.Linq;

namespace DynamicServices.Configuration
{
	public interface IDataProvider
	{
		object GetAllAsQueryable(Type type);
	}
}