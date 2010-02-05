namespace ReflectedAutoMap.Mapping
{
	using System;
	using AutoMapper;

	public class DynamicAutoMapperObjectMapper : IMapper
	{
		public bool HasMap(object source, Type sourceType, Type destinationType)
		{
			return true;
		}

		public object Map(object source, Type sourceType, Type destinationType)
		{
			return Mapper.DynamicMap(source, sourceType, destinationType);
		}
	}
}