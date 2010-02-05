using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DynamicServices.Pipeline
{
	public interface IServiceInvoker
	{
		object GetQueryableDataFor(Type type);
	}
}