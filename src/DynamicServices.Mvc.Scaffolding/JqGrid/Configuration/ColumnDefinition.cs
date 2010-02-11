using System.Reflection;
using System.Security.Principal;

namespace DynamicServices.Mvc.Scaffolding.JqGrid.Configuration
{
	public class ColumnDefinition
	{
		public IPrincipal User { get; set; }
		public PropertyInfo PropertyInfo { get; set; }
	}
}