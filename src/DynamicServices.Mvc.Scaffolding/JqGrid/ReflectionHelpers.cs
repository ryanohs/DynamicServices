using System.Linq.Expressions;
using System.Reflection;

namespace DynamicServices.Mvc.Scaffolding.JqGrid
{
	public static class ReflectionHelpers
	{
		public static PropertyInfo FindPropertyFromExpression(LambdaExpression lambdaExpression)
		{
			Expression expressionToCheck = lambdaExpression;

			bool done = false;

			while (!done)
			{
				switch (expressionToCheck.NodeType)
				{
					case ExpressionType.Convert:
						expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
						break;
					case ExpressionType.Lambda:
						expressionToCheck = lambdaExpression.Body;
						break;
					case ExpressionType.MemberAccess:
						var propertyInfo = ((MemberExpression)expressionToCheck).Member as PropertyInfo;
						return propertyInfo;
					default:
						done = true;
						break;
				}
			}

			return null;
		}
	}
}