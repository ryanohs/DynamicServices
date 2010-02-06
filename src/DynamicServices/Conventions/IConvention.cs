using System.Reflection;

namespace DynamicServices.Conventions
{
	public interface IConvention
	{
		bool Matches(object filter, PropertyInfo propertyInfo);
	}
}