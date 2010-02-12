using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FubuMVC.UI;

namespace Mvc.Application
{
	public class HtmlConventionRegistry
	{
		public static void Register(IWindsorContainer container)
		{
			container.Register(Component.For<HtmlConventions>().ImplementedBy<DefaultHtmlConventions>());
		}
	}
}