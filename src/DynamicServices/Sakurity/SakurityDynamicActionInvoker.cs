namespace DynamicServices.Sakurity
{
	using System;
	using System.Collections.Generic;
	using Pagination;

	public class SakurityDynamicActionInvoker : IDynamicActionInvoker
	{
		private readonly IDynamicActionInvoker _Invoker;
		private readonly ISakurityOffica _Offica;

		public SakurityDynamicActionInvoker(ISakurityOffica offica, PaginationStage invoker)
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

		public IList<DynamicParameter> GetStageParameters(DynamicAction action)
		{
			return _Invoker.GetStageParameters(action);
		}
	}
}