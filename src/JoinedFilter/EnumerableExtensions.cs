namespace JoinedFilter
{
	using System;
	using System.Collections.Generic;

	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if(source == null || action == null)
			{
				return;
			}

			foreach(var item in source)
			{
				action(item);
			}
		}
	}
}