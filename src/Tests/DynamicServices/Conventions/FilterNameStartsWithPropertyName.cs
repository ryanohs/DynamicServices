using DynamicServices.Conventions;
using NUnit.Framework;

namespace Tests.DynamicServices.Conventions
{
	[TestFixture]
	public class FilterNameStartsWithPropertyNameTests : AssertionHelper
	{
		private class ValuedCustomersFilter
		{}
	
		[Test]
		public void Matches_PropertyNameMatchesMethodName_ReturnsTrue()
		{
			var propertyName = "ValuedCustomers";
			var filter = new ValuedCustomersFilter();
			var convention = new FilterNameStartsWithPropertyName();

			var matches = convention.Matches(filter, propertyName);
			
			Expect(matches, Is.True);
		}	
	}
}