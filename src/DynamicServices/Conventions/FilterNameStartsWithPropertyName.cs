using System.Reflection;

namespace DynamicServices.Conventions
{
	public class FilterNameStartsWithPropertyName : IConvention
	{
		public bool Matches(object filter, PropertyInfo propertyInfo)
		{
			return filter.GetType().Name.StartsWith(propertyInfo.Name);
		}
	}
}