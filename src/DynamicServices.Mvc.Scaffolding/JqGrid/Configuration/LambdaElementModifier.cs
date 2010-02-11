using System;

namespace DynamicServices.Mvc.Scaffolding.JqGrid.Configuration
{
	public class LambdaElementModifier : IColumnModifier
	{
		private readonly Func<ColumnDefinition, bool> _matches;
		private readonly Func<ColumnDefinition, ColumnModifier> _modifierBuilder;

		public LambdaElementModifier(Func<ColumnDefinition, bool> matches, Func<ColumnDefinition, ColumnModifier> modifierBuilder)
		{
			_matches = matches;
			_modifierBuilder = modifierBuilder;
		}

		public ColumnModifier CreateModifier(ColumnDefinition accessorDef)
		{
			return _matches(accessorDef) ? _modifierBuilder(accessorDef) : null;
		}
	}
}