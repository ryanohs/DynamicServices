namespace DynamicServices.Mvc
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Web.Mvc;
	using Microsoft.Practices.ServiceLocation;

	public class ReflectedCommand : DynamicActionDescriptor
	{
		private readonly IServiceLocator _Locator;
		private MethodInfo _CommandMethod;

		public ReflectedCommand(IServiceLocator locator)
		{
			_Locator = locator;
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			var commandType = _CommandMethod.DeclaringType;
			var repositoryType = typeof (IDynamicRepository<>).MakeGenericType(new[] {commandType});
			var repository = _Locator.GetInstance(repositoryType);
			var getCommand = repositoryType.GetMethod("Get");
			var command = getCommand.Invoke(repository, new[] {parameters["id"]});
			if(command == null)
			{
				throw new Exception("Entity not found");
			}
			_CommandMethod.Invoke(command, parameters.Where(p => p.Key != "id").OfType<object>().ToArray());
			return new EmptyResult();
		}

		public void SetCommandMethod(MethodInfo commandMethod)
		{
			_CommandMethod = commandMethod;
			_Parameters.Clear();
			AddParameter("id", typeof (object));
			AddParameters(commandMethod.GetParameters());
		}
	}
}