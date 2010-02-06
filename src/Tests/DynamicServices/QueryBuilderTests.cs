using DynamicServices.Filters;
using NUnit.Framework;
using Tests.Model;

namespace Tests.DynamicServices
{
	[TestFixture]
	public class QueryBuilderTests : AssertionHelper
	{
		[Test]
		public void InvokeQuery_ProductIsInStock_QueryReturnsTrue()
		{
			var product = new Product() {InStock = true};

			var result = new QueryBuilder().InvokeQuery(product, "InStock");

			Expect(result, Is.True);
		}

		[Test]
		public void InvokeQuery_ProductIsNotInStock_QueryReturnsFalse()
		{
			var product = new Product() { InStock = false };

			var result = new QueryBuilder().InvokeQuery(product, "InStock");

			Expect(result, Is.False);
		}
	}
}