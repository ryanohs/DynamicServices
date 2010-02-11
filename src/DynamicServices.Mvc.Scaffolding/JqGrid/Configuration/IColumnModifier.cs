namespace DynamicServices.Mvc.Scaffolding.JqGrid.Configuration
{
	public interface IColumnModifier
	{
		ColumnModifier CreateModifier(ColumnDefinition accessorDef);
	}
}