using System;

namespace DynamicServices.Mvc.Scaffolding.JqGrid.Configuration
{
	public class ColumnActionExpression : ColumnFactoryExpression
	{
		private readonly ColumnFactory _factory;
		private readonly Func<ColumnDefinition, bool> _matches;

		public ColumnActionExpression(ColumnFactory factory, Func<ColumnDefinition, bool> matches) : base(factory)
		{
			_factory = factory;
			_matches = matches;
		}

		public void Modify(ColumnModifier modifier)
		{
			_factory.AddModifier(new LambdaElementModifier(_matches, def => modifier));
		}

		public void Name(string name)
		{
			Modify((r, column) => column.Name(name));
		}

		public void Index(string index)
		{
			Modify((r, column) => column.Index(index));
		}

		public void Width(int width)
		{
			Modify((r, column) => column.Width(width));
		}

		public void NotSortable()
		{
			Modify((r, column) => column.NotSortable());
		}

		public void Sortable()
		{
			Modify((r, column) => column.Sortable());
		}

		public void Hide()
		{
			Modify((r, column) => column.Hide());
		}

		public void Show()
		{
			Modify((r, column) => column.Show());
		}
	}
}