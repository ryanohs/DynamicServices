using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Tests.Model;

namespace Tests.Repositories
{
	public class FakeRepository<T> : IRepository<T>
		where T : Entity, new()
	{
		public IQueryable<T> All
		{
			get
			{
				var result = new List<T>();
				Enumerable.Range(1, 10).ForEach(id => result.Add(new T
				                                                 	{
				                                                 		Id = id
				                                                 	}));
				if(typeof(T) == typeof(Product))
				{
					result.Cast<Product>().ForEach(p => p.InStock = p.Id < 8);
				}
				return result.AsQueryable();
			}
		}
	}
}