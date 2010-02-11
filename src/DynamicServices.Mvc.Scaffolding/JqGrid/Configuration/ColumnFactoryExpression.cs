using System;
using System.Linq;

namespace DynamicServices.Mvc.Scaffolding.JqGrid.Configuration
{
	public class ColumnFactoryExpression
	{
		private readonly ColumnFactory _factory;

		public ColumnFactoryExpression(ColumnFactory factory)
		{
			_factory = factory;
		}

		public ColumnActionExpression If(Func<ColumnDefinition, bool> matches)
		{
			return new ColumnActionExpression(_factory, matches);
		}

		public ColumnActionExpression IfPropertyNameIs(string name)
		{
			return If(def => def.PropertyInfo.Name == name);
		}

		public ColumnActionExpression IfUserHasRole(string role)
		{
			return If(def => def.User.IsInRole(role));
		}

		public ColumnActionExpression IfTypeIs<T>()
		{
			return If(def => def.PropertyInfo.PropertyType == typeof (T));
		}

		public ColumnActionExpression HasAttribute<T>()
		{
			return If(def => def.PropertyInfo.GetCustomAttributes(true).Any(a => a.GetType() == typeof (T)));
		}

		public ColumnActionExpression Always
		{
			get
			{
				return If(def => true);
			}
		}

		public ColumnActionExpression Default
		{
			get
			{
				return If(def => true);
			}
		}
	}
}