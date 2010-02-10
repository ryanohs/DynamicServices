using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace DynamicServices.Pipeline
{
	public static class Utilities
	{
		public static object ToList(Type innerType, object enumerable)
		{
			AssertIsNotNull(enumerable);
			AssertIsEnumerable(enumerable);
			var enumerableType = typeof(Enumerable);
			var toList = enumerableType.GetMethod("ToList");
			var method = toList.MakeGenericMethod(innerType);
			return method.Invoke(null, new[] { enumerable });
		}

		public static object ToPagedList(object enumerable)
		{
			AssertIsNotNull(enumerable);
			AssertIsEnumerable(enumerable);
			var innerType = enumerable.GetType().GetGenericArguments()[0];
			return ToPagedList(innerType, enumerable);
		}

		public static object ToPagedList(Type innerType, object enumerable)
		{
			AssertIsNotNull(enumerable);
			AssertIsEnumerable(enumerable);
			var pagedListType = typeof(PagedListExtensions);
			var toPagedList = pagedListType.GetMethod("ToPagedList");
			var method = toPagedList.MakeGenericMethod(innerType);
			return method.Invoke(null, new[] { enumerable, 0, 5 });		// TODO PagingCriteria
		}

		public static void AssertIsNotNull(object argument)
		{
			if (argument == null)
			{
				throw new ArgumentException("Argument is null.");
			}
		}

		public static void AssertIsEnumerable(object argument)
		{
			var type = argument.GetType();
			if (type.IsGenericType)
			{
				var outerType = type.GetGenericTypeDefinition();
				if (typeof(IEnumerable<>).IsAssignableFrom(outerType) || typeof(IEnumerable).IsAssignableFrom(outerType))	// Second case handles "System.Linq.EnumerableQuery" type
				{
					return;
				}
			}
			throw new ArgumentException("Argument is not enumerable.");
		}

		public static void AssertPropertyExists(Type type, string propertyName)
		{
			if(type.GetProperty(propertyName) == null)
			{
				throw new ArgumentException(string.Format("Type {0} does not have a property named '{1}'", type.FullName, propertyName));
			}
		}
	}
}