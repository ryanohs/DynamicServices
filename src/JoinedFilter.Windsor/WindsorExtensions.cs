namespace JoinedFilter.Windsor
{
	using System;
	using System.Reflection;
	using Castle.MicroKernel;
	using Castle.MicroKernel.ComponentActivator;

	/// <summary>
	/// This is used from http://www.jeremyskinner.co.uk/2008/11/08/dependency-injection-with-aspnet-mvc-action-filters/
	/// </summary>
	public static class WindsorExtension
	{
		public static void InjectProperties(this IKernel kernel, object target)
		{
			var type = target.GetType();
			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				if (property.CanWrite && kernel.HasComponent(property.PropertyType))
				{
					var value = kernel.Resolve(property.PropertyType);
					try
					{
						property.SetValue(target, value, null);
					}
					catch (Exception ex)
					{
						var message = string.Format("Error setting property {0} on type {1}, See inner exception for more information.", property.Name, type.FullName);
						throw new ComponentActivatorException(message, ex);
					}
				}
			}
		}
	}
}