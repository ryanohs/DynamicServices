using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DynamicServices.Pipeline;

namespace DynamicServices.Filters
{
	public class QueryBuilder
	{
		// If we're only supporting boolean properties we could just check if the property is true!
		public object GetQueryFrom(Type genericType, string propertyName)
		{
			Utilities.AssertPropertyExists(genericType, propertyName);
		
			var input = Expression.Parameter(genericType, "input");
			var property = Expression.Property(input, propertyName);
			var cTrue = Expression.Constant(true, typeof(bool));
			var isTrue = Expression.Equal(property, cTrue);

			var funcType = typeof(Func<,>).MakeGenericType(new[] { genericType, typeof(bool) });
			var lambdaFactory = typeof(Expression).GetMethod("Lambda", new[] { typeof(Expression), typeof(IEnumerable<ParameterExpression>) }).MakeGenericMethod(funcType);
			var lambda = lambdaFactory.Invoke(null, new object[] { isTrue, new[] { input } });

			// We've just built:
			//var lambda = Expression.Lambda<Func<[genericType], bool>>(isTrue, new ParameterExpression[] { input });
			return lambda;
		}
		
		public bool InvokeQuery(object input, string propertyName)
		{
			var query = GetQueryFrom(input.GetType(), propertyName);
			var compiledQuery = CompileLambda(query);
			return (bool)compiledQuery.GetType().GetMethod("Invoke").Invoke(compiledQuery, new[] { input });
		}

		private object CompileLambda(object lambda)
		{
			return lambda.GetType().GetMethod("Compile").Invoke(lambda, null);
		}
	}
}