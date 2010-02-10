namespace DynamicServices.Mvc
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Web.Mvc;
	using ActionDescriptors;

	public abstract class DynamicActionDescriptor : ActionDescriptor
	{
		private string _ActionName;
		protected IList<ParameterDescriptor> _Parameters;
		protected ControllerDescriptor ControllerDescriptorInternal;

		public DynamicActionDescriptor()
		{
			_Parameters = new List<ParameterDescriptor>();
		}

		public override ControllerDescriptor ControllerDescriptor
		{
			get { return ControllerDescriptorInternal; }
		}

		public override string ActionName
		{
			get { return _ActionName; }
		}

		public override ParameterDescriptor[] GetParameters()
		{
			return _Parameters.ToArray();
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
			_Parameters.Add(new DynamicParameterDescriptor(this, parameter.Name, parameter.Type));
		}

		public virtual void AddParameters(IDynamicStage invoker, DynamicAction action)
		{
			invoker.GetStageParameters(action).ToList()
				.ForEach(AddParameter);
		}
	}
}