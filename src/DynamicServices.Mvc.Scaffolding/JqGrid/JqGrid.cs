using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace DynamicServices.Mvc.Scaffolding.JqGrid
{
	using System.Web.Script.Serialization;
	using Configuration;

	public class JqGrid : IJqGridColumnBuilder
	{
		#region Privates

		private const string _defaultAction = "all";
		private string _action;
		private string _controller;
		private string _url;
		private string _jsonToMerge;
		private JqGridColumn _lastColumn;

		#endregion

		#region Properties To Serialize

		public string url
		{
			get { return _url ?? _controller + "/" + _action; }
		}

		public IList<JqGridColumn> colModel { get; set; }

		public IEnumerable<string> colNames
		{
			get { return colModel.Select(c => c.GetHeader()); }
		}

		public string sortname { get; set; }
		public string caption { get; set; }
		public int? width { get; set; }
		public int? height { get; set; }

		#endregion

		public JqGrid()
		{
			_action = _defaultAction;
			colModel = new List<JqGridColumn>();
			width = 500;
			height = 300;
		}

		public JqGrid Helper(HtmlHelper helper)
		{
			_controller = helper.ViewContext.RouteData.Values["controller"].ToString();
			return this;
		}

		#region IJqGridBuilder
		public IJqGridBuilder Controller(string controller)
		{
			_controller = controller;
			return this;
		}

		public IJqGridBuilder Action(string action)
		{
			if (_url != null)
			{
				throw new Exception("You can only specify Source() or Action(), not both.");
			}
			_action = action;
			return this;
		}

		public IJqGridBuilder Source(string url)
		{
			if (_action != _defaultAction)
			{
				throw new Exception("You can only specify Source() or Action(), not both.");
			}
			_url = url;
			return this;
		}

		public IJqGridBuilder ShowEdit()
		{
			colModel.Add(new JqGridColumn() { name = "Edit" });
			return this;
		}

		public IJqGridBuilder ShowDelete()
		{
			colModel.Add(new JqGridColumn() { name = "Delete" });
			return this;
		}

		public IJqGridBuilder Caption(string caption)
		{
			this.caption = caption;
			return this;
		}

		public IJqGridBuilder Width(int width)
		{
			this.width = width;
			return this;
		}

		public IJqGridBuilder Height(int height)
		{
			this.height = height;
			return this;
		}

		public IJqGridBuilder SortBy<T>(Expression<Func<T, object>> expression)
		{
			var property = ReflectionHelpers.FindPropertyFromExpression(expression);
			this.sortname = property.Name;
			return this;
		}

		public IJqGridBuilder AutoColumns<T>()
		{
			return AutoColumns(typeof(T));
		}

		public IJqGridBuilder AutoColumns(Type type)
		{
			var properties = type.GetProperties();
			properties.Where(p => p.Name != "Id").Select(p => new JqGridColumn() { name = p.Name, index = p.Name }).ToList().ForEach(colModel.Add);
			return this;
		}

		public IJqGridColumnBuilder AddColumn<T>(Expression<Func<T, object>> expression)
		{
			var property = ReflectionHelpers.FindPropertyFromExpression(expression);
			AddColumn(property.Name);
			return this;
		}

		public IJqGridColumnBuilder AddColumn(string name)
		{
			_lastColumn = new JqGridColumn() { name = name, index = name};
			colModel.Add(_lastColumn);
			return this;
		}

		public IJqGridBuilder Merge(string json)
		{
			_jsonToMerge = json;
			return this;
		} 
		#endregion

		#region IJqGridColumnBuilder
		public IJqGridBuilder Configure(Action<JqGridColumn> configuration)
		{
			configuration(_lastColumn);
			return this;
		} 
		#endregion

		public override string ToString()
		{
			var merge = _jsonToMerge != null;
			return WriteHtml() +
			       "<script type='text/javascript'>$(document).ready(function() {" +
			       "	Grid(" +
			       (merge ? "merge(" : string.Empty) +
			       ConfigurationToJson() +
			       (merge ? string.Format(", {0})", _jsonToMerge) : string.Empty) +
			       ").navGrid('Pager', defaultNavGridJson(), {}, {}, { url: 'delete'});" +
			       "});</script>";
		}

		private string WriteHtml()
		{
			return "<table id='Grid' class='scroll' cellpadding='0' cellspacing='0'>" +
			       "</table><div id='Pager' class='scroll' style='text-align: center;'></div>";
		}

		private string ConfigurationToJson()
		{
			return new JavaScriptSerializer().Serialize(this);
		}
	}
}