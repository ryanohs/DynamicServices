namespace DynamicServices.Sakurity
{
	using System.Collections.Generic;

	public class SakurityDynamicActionInvoker : IDynamicActionInvoker
	{
		private readonly DomainActionInvoker _Invoker;
		private readonly ISakurityOffica _Offica;

		public SakurityDynamicActionInvoker(ISakurityOffica offica, DomainActionInvoker invoker)
		{
			// Todo need to put pipeline together and just have a next continuation like fubumvc.
			_Offica = offica;
			_Invoker = invoker;
		}

		public object Invoke(DynamicAction action, IDictionary<string, object> parameters)
		{
			_Offica.SakuritySakurity(action);
			return _Invoker.Invoke(action, parameters);
		}
	}
}