namespace DynamicServices.Mvc
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	using ActionDescriptors;
	using Menus;
	using Microsoft.Practices.ServiceLocation;

	public abstract class DynamicActionDescriptor : ActionDescriptor
	{
		public IServiceLocator Locator { get; set; }
		private string _ActionName;
		protected IList<ParameterDescriptor> Parameters;
		protected ControllerDescriptor ControllerDescriptorInternal;

		public DynamicActionDescriptor()
		{
			Parameters = new List<ParameterDescriptor>();
		}

		public override ControllerDescriptor ControllerDescriptor
		{
			get { return ControllerDescriptorInternal; }
		}

		public override string ActionName
		{
			get { return _ActionName; }
		}

		public override FilterInfo GetFilters()
		{
			var filters = base.GetFilters();
			filters.ActionFilters.Add(new JqGridInterceptorFilter());
			filters.ResultFilters.Add(Locator.GetInstance<InjectMenuResultFilter>());
			return filters;
		}

		public override ParameterDescriptor[] GetParameters()
		{
			return Parameters.ToArray();
		}

		public virtual void SetControllerDescriptor(ControllerDescriptor controllerDescriptor)
		{
			ControllerDescriptorInternal = controllerDescriptor;
		}

		public virtual void SetActionName(string actionName)
		{
			_ActionName = actionName.ToLowerInvariant();
		}

		public virtual void AddParameter(DynamicParameter parameter)
		{
			Parameters.Add(new DynamicParameterDescriptor(this, parameter.Name, parameter.Type));
		}

		public virtual void AddParameters(IDynamicStage invoker, DynamicAction action)
		{
			invoker.GetParameters(action).ToList()
				.ForEach(AddParameter);
		}
	}
}