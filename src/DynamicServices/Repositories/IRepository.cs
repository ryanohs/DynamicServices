using System;
using System.Linq;
using DynamicServices.Model;

namespace DynamicServices.Repositories
{
	public interface IRepository<T>
		where T : Entity, new()
	{
		IQueryable<T> All { get; }
	}
}