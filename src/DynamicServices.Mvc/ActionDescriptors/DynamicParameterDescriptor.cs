namespace DynamicServices.Mvc.ActionDescriptors
{
	using System;
	using System.Web.Mvc;

	public class DynamicParameterDescriptor : ParameterDescriptor
	{
		private readonly ActionDescriptor _ActionDescriptor;
		private readonly string _ParameterName;
		private readonly Type _ParameterType;

		public DynamicParameterDescriptor(ActionDescriptor actionDescriptor, string parameterName, Type parameterType)
		{
			_ActionDescriptor = actionDescriptor;
			_ParameterName = parameterName;
			_ParameterType = parameterType;
		}

		public override ActionDescriptor ActionDescriptor
		{
			get { return _ActionDescriptor; }
		}

		public override string ParameterName
		{
			get { return _ParameterName; }
		}

		public override Type ParameterType
		{
			get { return _ParameterType; }
		}
	}
}