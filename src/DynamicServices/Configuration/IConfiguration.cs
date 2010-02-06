using System;

namespace DynamicServices.Configuration
{
	public interface IConfiguration
	{
		IDataProvider DataProvider { get; }
	}
}