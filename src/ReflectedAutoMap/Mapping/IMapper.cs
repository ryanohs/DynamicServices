namespace ReflectedAutoMap.Mapping
{
	using System;

	public interface IMapper
	{
		bool HasMap(object source, Type sourceType, Type destinationType);
		object Map(object source, Type sourceType, Type destinationType);
	}
}