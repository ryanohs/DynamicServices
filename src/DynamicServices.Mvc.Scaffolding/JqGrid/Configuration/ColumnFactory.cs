using System.Collections.Generic;
using System.Linq;

namespace DynamicServices.Mvc.Scaffolding.JqGrid.Configuration
{
	public class ColumnFactory
	{
		private IList<IColumnModifier> _modifiers;

		public ColumnFactory()
		{
			_modifiers = new List<IColumnModifier>();
		}

		public void AddModifier(IColumnModifier modifier)
		{
			_modifiers.Add(modifier);
		}
		
		public JqGridColumn Build(ColumnDefinition definition)
		{
			var column = new JqGridColumn();
			_modifiers.Select(m => m.CreateModifier(definition)).Where(x => x != null).ToList().ForEach(x => x(definition, column));
			return column;
		}
	}
}