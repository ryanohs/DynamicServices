namespace DynamicServices.Pagination
{
	public class PagingCriteria
	{
		public PagingCriteria()
		{
			Page = 1;
			PageSize = 1;
			//Todo we need contract requirements in here so page size and page are valid always
		}
		public int PageIndex
		{
			get { return Page - 1; }
		}

		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}