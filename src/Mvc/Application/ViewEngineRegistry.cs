namespace Mvc.Application
{
	using System.Web.Mvc;
	using Spark.Web.Mvc;

	public class ViewEngineRegistry
	{
		public static void SetViewEngines()
		{
			ViewEngines.Engines.Add(new SparkViewFactory());
		}
	}
}