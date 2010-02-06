using System;
using System.Collections;
using System.Reflection;

namespace DynamicServices.Pipeline
{
	public interface IFilterLocator
	{
		IEnumerable GetFiltersByConvention(Type type, PropertyInfo propertyInfo);
	}
}