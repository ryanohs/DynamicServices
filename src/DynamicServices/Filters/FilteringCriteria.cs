namespace DynamicServices.Filters
{
	using System;

	public class FilteringCriteria
	{
		private string _Filter;

		public string Filter
		{
			get { return _Filter ?? JqGridSearch(); }
			set { _Filter = value; }
		}

		public string SearchString { get; set; }
		public string SearchOper { get; set; }
		public string SearchField { get; set; }

		private string JqGridSearch()
		{
			if(string.IsNullOrEmpty(SearchField) && string.IsNullOrEmpty(SearchField) && string.IsNullOrEmpty(SearchOper))
			{
				return null;
			}
			return SearchField + " " + JqGridOperator() + " " + SearchString;
		}

		private string JqGridOperator()
		{
			switch (SearchOper)
			{
				case "eq":
					return "=";
				case "ne":
					return "!=";
				case "ge":
					return ">=";
				case "le":
					return "<=";
				case "gt":
					return ">";
				case "lt":
					return "<";
			}
			throw new NotImplementedException("Unsupported comparison");
		}
	}
}