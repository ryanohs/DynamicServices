namespace DynamicServices.Mvc
{
	using System.Web.Mvc;

	public interface IDynamicQuery
	{
		object GetData(ControllerContext context, QueryParameters parameters);
	}
}