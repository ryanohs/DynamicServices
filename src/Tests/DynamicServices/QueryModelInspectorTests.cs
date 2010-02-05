using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DynamicServices.Filters;
using DynamicServices.Model;
using DynamicServices.Pipeline;
using DynamicServices.Repositories;
using NUnit.Framework;
using System.Collections.Generic;
using PagedList;
using Rhino.Mocks;

namespace Tests.DynamicServices
{
	[TestFixture]
	public class QueryModelInspectorTests : AssertionHelper
	{
		#region Test Model Classes
		public class TwoPropertiesTestViewModel
		{
			public IPagedList<Product> Products { get; set; }
			public IEnumerable<Customer> Customers { get; set; }
		}

		public class PagedViewModel
		{
			public IPagedList<Customer> Customers { get; set; }
		}

		public class EnumerableViewModel
		{
			public IEnumerable<Customer> Customers { get; set; }
		}

		public class ListViewModel
		{
			public IList<Customer> Customers { get; set; }
		}
		
		public class InStockProductsViewModel
		{
			public IList<Product> InStockProducts { get; set; }
		}
		#endregion

		// NOTE this test is basically redundant at this point.
		// But it does demonstrate a view model with two properties being filled.
		[Test]
		public void Fill_popultes_the_model_with_the_expected_data()
		{
			var viewModel = new TwoPropertiesTestViewModel();
			var invoker = MockRepository.GenerateStub<IServiceInvoker>();
			var container = MockRepository.GenerateStub<IWindsorContainer>();
			container.Stub(c => c.ResolveAll(typeof (object))).IgnoreArguments().Return(new object[0]);
			var inspector = new QueryModelInspector()
								{
									ServiceInvoker = invoker,
									Container = container
								};
			invoker.Stub(i => i.GetQueryableDataFor(typeof(Product)))
				.Return(new List<Product>()
		                    {
		                        new Product()
		                    }.ToPagedList(0, 20));
			invoker.Stub(i => i.GetQueryableDataFor(typeof(Customer)))
				.Return(new List<Customer>()
		                    {
		                        new Customer(),
		                        new Customer()
		                    });

			inspector.Fill(viewModel, null);

			Expect(viewModel.Products, Is.Not.Null);
			Expect(viewModel.Products.Count(), Is.EqualTo(1));
			Expect(viewModel.Customers, Is.Not.Null);
			Expect(viewModel.Customers.Count(), Is.EqualTo(2));
		}

		[Test]
		public void Fill_NonNullProperyIsNotSet()
		{
			var container = CreateContainer();
			var inspector = container.Resolve<QueryModelInspector>();
			var viewModel = new ListViewModel()
			                	{
			                		Customers = new List<Customer>()
			                	};

			inspector.Fill(viewModel, null);

			Expect(viewModel.Customers, Is.Not.Null);
			Expect(viewModel.Customers.Count(), Is.EqualTo(0));
		}

		[Test]
		public void Fill_ListViewModelWithNullList_ListPropertySet()
		{
			var container = CreateContainer();
			var inspector = container.Resolve<QueryModelInspector>();
			var viewModel = new ListViewModel();

			inspector.Fill(viewModel, null);

			Expect(viewModel.Customers, Is.Not.Null);
			Expect(viewModel.Customers.Count(), Is.EqualTo(10));
		}

		[Test]
		public void Fill_EnumerableViewModelWithNullList_EnumerablePropertySet()
		{
			var container = CreateContainer();
			var inspector = container.Resolve<QueryModelInspector>();
			var viewModel = new EnumerableViewModel();

			inspector.Fill(viewModel, null);

			Expect(viewModel.Customers, Is.Not.Null);
			Expect(viewModel.Customers.Count(), Is.EqualTo(10));
		}

		[Test]
		public void Fill_PagedViewModelWithNullList_PagedPropertySet()
		{
			var container = CreateContainer();
			var inspector = container.Resolve<QueryModelInspector>();
			var viewModel = new PagedViewModel();

			inspector.Fill(viewModel, null);

			Expect(viewModel.Customers, Is.Not.Null);
			Expect(viewModel.Customers.Count(), Is.EqualTo(5));
		}

		[Test]
		public void Fill_PropertyMatchesFilterByStartsWithNameConvention_AppliesFilter()
		{
			var container = CreateContainer();
			var inspector = container.Resolve<QueryModelInspector>();
			var viewModel = new InStockProductsViewModel();

			inspector.Fill(viewModel, null);

			Expect(viewModel.InStockProducts, Is.Not.Null);
			Expect(viewModel.InStockProducts.Count(), Is.EqualTo(7));
		}

		private IWindsorContainer CreateContainer()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<IWindsorContainer>().Instance(container));
			container.Register(Component.For<QueryModelInspector>().ImplementedBy<QueryModelInspector>());
			container.Register(Component.For<IServiceInvoker>().ImplementedBy<ServiceInvoker>());
			container.Register(Component.For(typeof (IRepository<>)).ImplementedBy(typeof (FakeRepository<>)));
			container.Register(AllTypes.FromAssemblyContaining<InStockProductsFilter>().BasedOn(typeof (IFilter<>)));
			return container;
		}
	}
}