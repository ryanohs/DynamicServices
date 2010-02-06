using System;
using System.Linq;
using DynamicServices.Filters;
using DynamicServices.Pipeline;
using NUnit.Framework;
using System.Collections.Generic;
using Tests.Model;

namespace Tests.DynamicServices
{
	[TestFixture]
	public class FilterExecutorTests : AssertionHelper
	{
		private class TestFilter : IFilter<Customer>
		{
			public bool ExecuteCalled { get; set; }

			public TestFilter()
			{
				ExecuteCalled = false;
			}

			public IQueryable<Customer> Execute(IQueryable<Customer> source)
			{
				ExecuteCalled = true;
				return source;
			}
		}

		[Test]
		public void Execute_ActingOnAFilter_CallsExecute()
		{
			var filter = new TestFilter();
			var source = new List<Customer>().AsQueryable();
			var executor = new FilterExecutor();

			executor.Execute(filter, source);

			Expect(filter.ExecuteCalled, Is.True);
		}
	}
}