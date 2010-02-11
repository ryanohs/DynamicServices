namespace DynamicServices.Mvc.Scaffolding.JqGrid.Configuration
{
	public class JqGridConfiguration
	{
		private readonly ColumnFactory _factory;
		
		public ColumnFactoryExpression Column { get; set; }

		public JqGridConfiguration(ColumnFactory factory)
		{
			_factory = factory;
			Column = new ColumnFactoryExpression(_factory);
		}
	}
}