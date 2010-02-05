namespace DynamicServices.Conventions
{
	public interface IConvention
	{
		bool Matches(object filter, string propertyName);
	}
}