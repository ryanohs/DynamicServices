namespace ReflectedAutoMap
{
	using System;
	using System.Web.Mvc;
	using Mapping;

	public class ReflectedAutoMapper : IReflectedAutoMapper
	{
		public IViewModelTypeReflector TypeReflector { get; set; }

		public IMapper ObjectMapper { get; set; }

		public ReflectedAutoMapper(IMapper objectMapper, IViewModelTypeReflector typeReflector)
		{
			TypeReflector = typeReflector;
			ObjectMapper = objectMapper;
		}

		public void TryMapSourceToDestination(ActionExecutedContext filterContext)
		{
			var sourceModel = filterContext.Controller.ViewData.Model;
			if (sourceModel == null)
			{
				return;
			}

			var destinationType = TypeReflector.GetDestinationModelType(filterContext);
			if (destinationType == null)
			{
				return;
			}

			var mapped = Map(sourceModel, destinationType);
			if (mapped != null)
			{
				filterContext.Controller.ViewData.Model = mapped;
			}
		}

		private object Map(object sourceModel, Type destinationType)
		{
			var sourceType = sourceModel.GetType();
			if (sourceType == destinationType
			    || !ObjectMapper.HasMap(sourceModel, sourceType, destinationType))
			{
				return null;
			}

			var destinationModel = ObjectMapper.Map(sourceModel, sourceType, destinationType);
			return destinationModel;
		}
	}
}