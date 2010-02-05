namespace Tests.Web.Core.Mapping
{
	using AutoMapper;
	using NUnit.Framework;
	using ReflectedAutoMap.Mapping;

	[TestFixture]
	public class AutoMapperObjectMapperTests : AssertionHelper
	{
		#region Setup/Teardown

		[SetUp]
		public void Setup()
		{
			Mapper.Reset();
		}

		#endregion

		private class Source
		{
		}

		private class Destination
		{
		}

		private AutoMapperObjectMapper NewAutomapperObjectMapper()
		{
			return new AutoMapperObjectMapper();
		}

		[Test]
		public void HasMap_NonNullSource_HasMap()
		{
			Mapper.CreateMap<Source, Destination>();
			var objectMapper = NewAutomapperObjectMapper();
			var source = new Source();

			var hasMap = objectMapper.HasMap(source, typeof (Source), typeof (Destination));

			Expect(hasMap);
		}

		[Test]
		public void HasMap_NonNullSourceCachedTypePair_HasMap()
		{
			Mapper.CreateMap<Source, Destination>();
			var objectMapper = NewAutomapperObjectMapper();
			var source = new Source();
			Mapper.Map(source, typeof (Source), typeof (Destination));

			var hasMap = objectMapper.HasMap(source, typeof(Source), typeof(Destination));

			Expect(hasMap);
		}

		[Test]
		public void HasMap_NullSource_HasMap()
		{
			var objectMapper = NewAutomapperObjectMapper();
			Source source = null;

			var hasMap = objectMapper.HasMap(source, typeof(Source), typeof(Destination));

			Expect(hasMap);
		}

		[Test]
		public void HasMap_NoMapping_DoesntHaveMap()
		{
			var objectMapper = NewAutomapperObjectMapper();
			var source = new Source();

			var hasMap = objectMapper.HasMap(source, typeof(Source), typeof(Destination));

			Expect(hasMap, Is.False);
		}
	
	}
}