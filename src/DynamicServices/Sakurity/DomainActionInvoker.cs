namespace DynamicServices.Sakurity
{
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.Practices.ServiceLocation;

	public class DomainActionInvoker : IDynamicActionInvoker
	{
		private readonly IServiceLocator _Locator;

		public DomainActionInvoker(IServiceLocator locator)
		{
			_Locator = locator;
		}

		public object Invoke(DynamicAction action, IDictionary<string, object> parameters)
		{
			object instance;
			if (action is EntityAction)
			{
				instance = GetEntity(action as EntityAction, parameters);
				parameters = parameters.Where(p => p.Key.ToLowerInvariant() != "id").ToDictionary(x => x.Key, x => x.Value);
			}
			else
			{
				instance = _Locator.GetInstance(action.Type);
			}

			return action.Method.Invoke(instance, parameters.Select(p => p.Value).ToArray());
		}

		private object GetEntity(EntityAction action, IDictionary<string, object> parameters)
		{
			var repositoryType = typeof (IDynamicRepository<>).MakeGenericType(action.Type);
			var getCommand = repositoryType.GetMethod("Get");
			var repository = _Locator.GetInstance(repositoryType);
			// Todo this seems to bypass our security checks for this method... possibly we should have a thing to check this one too.  Not sure how, maybe checking access should be a reusable service instead of a pipeline component.
			// I would really like to have a stage to do this repository to entity fetching, maybe a DomainRepositoryActionInvoker stage?
			return getCommand.Invoke(repository,
			                         parameters.Where(p => p.Key.ToLowerInvariant() == "id").Select(p => p.Value).ToArray());
		}
	}
}