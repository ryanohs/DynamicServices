using System;
using System.Linq.Expressions;

namespace DynamicServices.Mvc.Scaffolding.JqGrid
{
	using Configuration;

	public interface IJqGridBuilder
	{
		IJqGridBuilder Action(string action);
		IJqGridBuilder Source(string url);
		IJqGridBuilder ShowEdit();
		IJqGridBuilder ShowDelete();
		IJqGridBuilder Caption(string caption);
		IJqGridBuilder Width(int width);
		IJqGridBuilder Height(int height);
		IJqGridBuilder Merge(string json);
		IJqGridBuilder SortBy<T>(Expression<Func<T, object>> expression);
		IJqGridBuilder AutoColumns<T>();
		IJqGridColumnBuilder AddColumn<T>(Expression<Func<T, object>> expression);
		IJqGridBuilder AutoColumns(Type type);
		IJqGridColumnBuilder AddColumn(string name);
		IJqGridBuilder Controller(string controller);
	}
}