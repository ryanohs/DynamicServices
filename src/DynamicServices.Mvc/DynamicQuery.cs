using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace DynamicServices.Mvc
{
	using System;
	using System.Web.Mvc;

	public class DynamicQuery : IDynamicQuery
	{
		public IServiceLocator Locator { get; set; }

		public object GetData(ControllerContext context, QueryParameters parameters)
		{
			var repositoryName = context.GetControllerName() + "repository";
			var repository =
				Locator.GetAllInstances(typeof(IDynamicRepository)).FirstOrDefault(
					o => o.GetType().Name.ToLowerInvariant() == repositoryName);
			if (repository == null)
			{
				throw new Exception(string.Format("Could not locate a repository named '{0}'", repositoryName));
			}
			return repository.GetType().GetMethod("All").Invoke(repository, null);
		}
	}
}