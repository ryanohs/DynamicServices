namespace DynamicServices.Conventions
{
	public class FilterNameStartsWithPropertyName : IConvention
	{
		public bool Matches(object filter, string propertyName)
		{
			return filter.GetType().Name.StartsWith(propertyName);
		}
	}
}