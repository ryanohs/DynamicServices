using System.Reflection;
using DynamicServices.Conventions;
using NUnit.Framework;

namespace Tests.DynamicServices.Conventions
{
	[TestFixture]
	public class FilterNameStartsWithPropertyNameTests : AssertionHelper
	{
		private class ValuedCustomersViewModel
		{
			public object ValuedCustomers { get; set; }
		}
	
		private class ValuedCustomersFilter
		{}
	
		[Test]
		public void Matches_PropertyNameMatchesMethodName_ReturnsTrue()
		{
			var propInfo = typeof (ValuedCustomersViewModel).GetProperty("ValuedCustomers");
			var filter = new ValuedCustomersFilter();
			var convention = new FilterNameStartsWithPropertyName();

			var matches = convention.Matches(filter, propInfo);
			
			Expect(matches, Is.True);
		}	
	}
}