using System;

namespace DynamicServices.Mvc.Scaffolding.JqGrid
{
	public class JqGridColumn
	{
		public string name { get; set; }
		public string index { get; set; }
		public bool sortable { get; set; }
		public int width { get; set; }
		public bool visible { get; set; }
		public string align { get; set; }
		public bool resizable { get; set; }

		private string _header;

		public JqGridColumn()
		{
			sortable = true;
			width = 80;
			visible = true;
			align = "left";
			resizable = true;
		}

		public JqGridColumn Name(string name)
		{
			this.name = name;
			return this;
		}

		public JqGridColumn Index(string index)
		{
			this.index = index;
			return this;
		}

		public JqGridColumn Header(string header)
		{
			_header = header;
			return this;
		}

		public string GetHeader()
		{
			return _header ?? name;
		}

		public JqGridColumn NotSortable()
		{
			sortable = false;
			return this;
		}

		public JqGridColumn Width(int width)
		{
			this.width = width;
			return this;
		}

		public JqGridColumn FixedWidth(int width)
		{
			this.width = width;
			this.resizable = false;
			return this;
		}

		public JqGridColumn Sortable()
		{
			this.sortable = true;
			return this;
		}

		public JqGridColumn Hide()
		{
			visible = false;
			return this;
		}

		public JqGridColumn Show()
		{
			visible = true;
			return this;
		}

		public JqGridColumn Left()
		{
			align = "left";
			return this;
		}

		public JqGridColumn Center()
		{
			align = "center";
			return this;
		}

		public JqGridColumn Right()
		{
			align = "right";
			return this;
		}
	}
}