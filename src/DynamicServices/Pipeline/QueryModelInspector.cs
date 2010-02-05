using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Windsor;
using DynamicServices.Filters;
using PagedList;

namespace DynamicServices.Pipeline
{
	public class QueryModelInspector
	{
		public IServiceInvoker ServiceInvoker { get; set; }
		public IWindsorContainer Container { get; set; }
	
		public void Fill(object viewModel, object inputModel)
		{
			var viewModelType = viewModel.GetType();
			var properties = viewModelType.GetProperties();
			foreach (var property in properties)
			{
				var currentValue = property.GetValue(viewModel, null);
				var propertyType = property.PropertyType;
				if (currentValue == null && propertyType.IsGenericType)
				{
					FillProperty(viewModel, property);
				}
			}
		}

		private void FillProperty(object viewModel, PropertyInfo property)
		{
			object result = null;
			var propertyType = property.PropertyType;
			var baseType = propertyType.GetGenericTypeDefinition();
			var innerType = propertyType.GetGenericArguments()[0];

			var data = ServiceInvoker.GetQueryableDataFor(innerType);

			var filters = GetFiltersByConvention(innerType, property.Name);
			foreach(var filter in filters)
			{
				data = filter.GetType().GetMethod("Execute").Invoke(filter, new[] { data });
			}
			
			if (baseType == typeof(IPagedList<>))
			{
				result = ToPagedList(innerType, data);
			}
			else if (baseType == typeof(IEnumerable<>) || baseType == typeof(IList<>))
			{
				result = ToList(innerType, data);
			}
			else if(baseType == typeof(IQueryable<>))
			{
				result = data;
			}
			
			property.SetValue(viewModel, result, null);
		}

		private IEnumerable GetFiltersByConvention(Type type, string propertyName)
		{
			var filterType = typeof (IFilter<>);
			var targetFilterType = filterType.MakeGenericType(type);
			var filters = (object[])Container.ResolveAll(targetFilterType);
			return filters.Where(f => ApplyStartsWithNameConvention(f, propertyName));
		}
		
		private bool ApplyStartsWithNameConvention(object filter, string propertyName)
		{
			return filter.GetType().Name.StartsWith(propertyName);
		}

		private object ToList(Type innerType, object enumerable)
		{
			AssertIsNotNull(enumerable);
			AssertIsEnumerable(enumerable);
			var enumerableType = typeof(Enumerable);
			var toList = enumerableType.GetMethod("ToList");
			var method = toList.MakeGenericMethod(innerType);
			return method.Invoke(null, new[] { enumerable });
		}

		private object ToPagedList(Type innerType, object enumerable)
		{
			AssertIsNotNull(enumerable);
			AssertIsEnumerable(enumerable);
			var pagedListType = typeof(PagedListExtensions);
			var toPagedList = pagedListType.GetMethod("ToPagedList");
			var method = toPagedList.MakeGenericMethod(innerType);
			return method.Invoke(null, new[] { enumerable, 0, 5 });		// TODO PagingCriteria
		}

		private void AssertIsNotNull(object argument)
		{
			if (argument == null)
			{
				throw new ArgumentException("Argument is null.");
			}
		}

		private void AssertIsEnumerable(object argument)
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
	}
}