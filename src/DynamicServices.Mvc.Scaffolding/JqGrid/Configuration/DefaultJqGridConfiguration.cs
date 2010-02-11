namespace DynamicServices.Mvc.Scaffolding.JqGrid.Configuration
{
	public class DefaultJqGridConfiguration : JqGridConfiguration
	{
		public DefaultJqGridConfiguration(ColumnFactory factory) : base(factory)
		{
			// These are executed in order; later config takes precidence over earlier.
			Defaults();
			ColumnVisibilityRules();
		}

		private void Defaults()
		{
			Column.Default.Width(80);
			Column.Default.Sortable();
		}

		private void ColumnVisibilityRules()
		{
			Column.Default.Show();
			Column.IfPropertyNameIs("Id").Hide();
			Column.IfPropertyNameIs("Id").IfUserHasRole("Admin").Show();
		}
	}
}