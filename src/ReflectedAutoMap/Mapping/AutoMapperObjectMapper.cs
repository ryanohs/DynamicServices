namespace ReflectedAutoMap.Mapping
{
	using System;
	using System.Collections;
	using System.Linq;
	using System.Reflection;
	using AutoMapper;

	public class AutoMapperObjectMapper : IMapper
	{
		public bool HasMap(object source, Type sourceType, Type destinationType)
		{
			if (source == null)
			{
				return true;
			}

			var resolutionContext = CreateResolutionContext(sourceType, destinationType, source);

			return IsCachedMapperFor(resolutionContext)
			       || IsMapperFor(resolutionContext);
		}

		private static bool IsMapperFor(ResolutionContext resolutionContext)
		{
			return Mapper.Engine._mappers().Any(mapper => mapper.IsMatch(resolutionContext));
		}

		private static bool IsCachedMapperFor(ResolutionContext resolutionContext)
		{
			var contextTypePair = CreateTypePair(resolutionContext.SourceType, resolutionContext.DestinationType);

			if (Mapper.Engine._objectMapperCache()[contextTypePair] != null)
			{
				return true;
			}
			return false;
		}

		private static ResolutionContext CreateResolutionContext(Type sourceType, Type destinationType, object source)
		{
			var typeMap = Mapper.FindTypeMapFor(sourceType, destinationType);

			return new ResolutionContext(typeMap, source, sourceType, destinationType);
		}

		private static object CreateTypePair(Type sourceType, Type destinationType)
		{
			var autoMapperAssembly = Assembly.GetAssembly(typeof (MappingEngine));
			return autoMapperAssembly.GetType("AutoMapper.Internal.TypePair").GetConstructors()[0].Invoke(new object[]
			                                                                                              {
			                                                                                              	sourceType,
			                                                                                              	destinationType
			                                                                                              });
		}

		public object Map(object source, Type sourceType, Type destinationType)
		{
			return Mapper.Map(source, sourceType, destinationType);
		}
	}

	public static class AutoMapperMappingEngineReflectedExtensions
	{
		public static IObjectMapper[] _mappers(this IMappingEngine engine)
		{
			return typeof (MappingEngine)
			       	.GetField("_mappers", BindingFlags.Instance | BindingFlags.NonPublic)
			       	.GetValue(engine) as IObjectMapper[];
		}

		public static IDictionary _objectMapperCache(this IMappingEngine engine)
		{
			return typeof (MappingEngine)
			       	.GetField("_objectMapperCache", BindingFlags.Instance | BindingFlags.NonPublic)
			       	.GetValue(engine) as IDictionary;
		}
	}
}