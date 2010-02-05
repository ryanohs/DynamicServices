using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PagedList;

namespace DynamicServices.Pipeline
{
	public class QueryModelInspector
	{
		public IServiceInvoker ServiceInvoker { get; set; }
		public IFilterLocator FilterLocator { get; set; }
	
		public void Fill(object viewModel, object inputModel)
		{
			var viewModelType = viewModel.GetType();
			var properties = viewModelType.GetProperties();
			foreach (var property in properties)
			{
				var currentValue = property.GetValue(viewModel, null);
				var propertyType = property.PropertyType;
				if (currentValue == null && propertyType.IsGenericType)
				{
					FillProperty(viewModel, property);
				}
			}
		}

		private void FillProperty(object viewModel, PropertyInfo property)
		{
			object result = null;
			var propertyType = property.PropertyType;
			var baseType = propertyType.GetGenericTypeDefinition();
			var innerType = propertyType.GetGenericArguments()[0];

			var data = ServiceInvoker.GetQueryableDataFor(innerType);

			var filters = FilterLocator.GetFiltersByConvention(innerType, property.Name);
			foreach(var filter in filters)
			{
				data = new FilterExecutor().Execute(filter, data);
			}
			
			if (baseType == typeof(IPagedList<>))
			{
				result = Utilities.ToPagedList(innerType, data);
			}
			else if (baseType == typeof(IEnumerable<>) || baseType == typeof(IList<>))
			{
				result = Utilities.ToList(innerType, data);
			}
			else if(baseType == typeof(IQueryable<>))
			{
				result = data;
			}
			
			property.SetValue(viewModel, result, null);
		}
	}
}