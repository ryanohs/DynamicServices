using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Castle.Windsor;
using DynamicServices.Conventions;
using DynamicServices.Filters;

namespace DynamicServices.Pipeline
{
	public class FilterLocator : IFilterLocator
	{
		public IWindsorContainer Container { get; set; }
	
		public IEnumerable GetFiltersByConvention(Type type, PropertyInfo propertyInfo)
		{
			var filterType = typeof(IFilter<>);
			var targetFilterType = filterType.MakeGenericType(type);
			var filters = (object[])Container.ResolveAll(targetFilterType);
			return filters.Where(f => new FilterNameStartsWithPropertyName().Matches(f, propertyInfo));
		}
	}
}