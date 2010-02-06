using System;
using System.Linq;
using DynamicServices.Pipeline;
using NUnit.Framework;
using System.Collections.Generic;
using PagedList;
using Tests.Model;

namespace Tests.DynamicServices
{
	[TestFixture]
	public class UtilitiesTests : AssertionHelper
	{
		[Test]
		public void ToList_SourceIsEnumerable_ReturnsList()
		{
			var enumerable = new List<Customer>().AsEnumerable();

			var list = Utilities.ToList(typeof(Customer), enumerable);

			Expect(list.GetType(), Is.EqualTo(typeof(List<Customer>)));
		}

		[Test]
		public void ToPagedList_SourceIsEnumerable_ReturnsPagedList()
		{
			var enumerable = new List<Customer>().AsEnumerable();

			var list = Utilities.ToPagedList(typeof(Customer), enumerable);

			Expect(list.GetType(), Is.EqualTo(typeof(PagedList<Customer>)));
		}

		[Test]
		public void AssertIsEnumerable_SourceIsEnumerable_DoesNothing()
		{
			var enumerable = new List<Customer>().AsEnumerable();

			TestDelegate act = () => Utilities.AssertIsEnumerable(enumerable);

			Expect(act, Throws.Nothing);
		}

		[Test]
		public void AssertIsEnumerable_SourceIsNotEnumerable_ThrowsArgumentException()
		{
			var notEnumerable = new object();

			TestDelegate act = () => Utilities.AssertIsEnumerable(notEnumerable);

			Expect(act, Throws.TypeOf<ArgumentException>());
		}

		[Test]
		public void AssertIsNotNull_SourceIsNull_ThrowsArgumentException()
		{
			object isNull = null;

			TestDelegate act = () => Utilities.AssertIsNotNull(isNull);

			Expect(act, Throws.TypeOf<ArgumentException>());
		}

		[Test]
		public void AssertIsNotNull_SourceIsNotNull_ThrowsNothing()
		{
			var isNotNull = new object();

			TestDelegate act = () => Utilities.AssertIsNotNull(isNotNull);

			Expect(act, Throws.Nothing);
		}
	}
}