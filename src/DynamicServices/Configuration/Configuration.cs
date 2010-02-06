namespace DynamicServices.Configuration
{
	public class Configuration : IConfiguration
	{
		public IDataProvider DataProvider { get; private set; }

		private Configuration()
		{
		}

		public static Configuration Default()
		{
			return new Configuration();
		}

		public Configuration SetDataProvider(IDataProvider provider)
		{
			DataProvider = provider;
			return this;
		}
	}
}