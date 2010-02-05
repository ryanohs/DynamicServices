using System;
using System.Collections;

namespace DynamicServices.Pipeline
{
	public interface IFilterLocator
	{
		IEnumerable GetFiltersByConvention(Type type, string propertyName);
	}
}