using System;

namespace DynamicServices.Mvc.Scaffolding.JqGrid.Configuration
{
	public interface IJqGridColumnBuilder : IJqGridBuilder
	{
		IJqGridBuilder Configure(Action<JqGridColumn> configuration);
	}
}