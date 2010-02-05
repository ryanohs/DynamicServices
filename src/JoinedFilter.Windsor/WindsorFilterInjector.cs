namespace JoinedFilter.Windsor
{
	using System.Web.Mvc;
	using Castle.Core;
	using Castle.Windsor;

	public class WindsorFilterInjector : IFilterInjector
	{
		public IWindsorContainer Container { get; set; }

		public WindsorFilterInjector(IWindsorContainer container)
		{
			Container = container;
		}

		public void BuildUp(FilterInfo filters)
		{
			CollectionExtensions.ForEach(filters.ActionFilters, Container.Kernel.InjectProperties);
			CollectionExtensions.ForEach(filters.AuthorizationFilters, Container.Kernel.InjectProperties);
			CollectionExtensions.ForEach(filters.ExceptionFilters, Container.Kernel.InjectProperties);
			CollectionExtensions.ForEach(filters.ResultFilters, Container.Kernel.InjectProperties);
		}
	}
}