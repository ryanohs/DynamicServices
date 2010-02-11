namespace DynamicServices.Sorting
{
	public class SortingCriteria
	{
		// Todo we need to find what we really want this to look like and have our model binders populate it?


		private string _Sort;

		public string Sort
		{
			get { return _Sort ?? Sidx + " " + Sord; }
			set
			{
				_Sort = value;
			}
		}

		/// <summary>
		/// Temp hack for jqgrid prototype
		/// </summary>
		public string Sidx { get; set; }

		public string Sord { get; set; }
	}
}