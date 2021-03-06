namespace Mvc.Application
{
	using DynamicServices;
	using DynamicServices.Sakurity;
	using Models;

	public class Sakurity : SakurityRegistry
	{
		public Sakurity()
		{
			DefaultLevel = 1;

			For<IDynamicRepository<Product>>()
				.Allow()
				.OnAll()
				.Everyone();

			For<ProductQueries>()
				.Allow()
				.OnAll()
				.Everyone();

			For<Product>()
				.Allow("ToString")
				.Everyone();

			For<LocationQueries>()
				.Allow()
				.OnAllQueries()
				.Everyone();
		}
	}
}