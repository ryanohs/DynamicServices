var baseUrl = $('base').attr('href');

// Defaults for jqGrid
var defaultPageSizes = [5, 10, 15, 20, 30, 40, 50, 100];
var defaultPageSize = 20;
var defaultPagerName = '#Pager';
var defaultGridName = '#Grid';

// Merge two json objects, takes a set of default values, and overwrites them with custom values and returns the result.
function merge (defaultValue, customValues) {
	for (var property in customValues) {
		defaultValue[property] = customValues[property];
	}
	return defaultValue;
}

// Default json grid settings
function Grid(gridJson, navbarJson) {
	var defaultJson = {	
		rowNum: defaultPageSize,
		rowList: defaultPageSizes,
		pager: defaultPagerName,
		datatype: "json",
		sortorder: "asc",
		viewrecords: true,
		jsonReader: { id: "Id", repeatitems: false },
	};
	
	gridJson = merge(defaultJson, gridJson);

	return jQuery(defaultGridName).jqGrid(gridJson);
}

function defaultNavGridJson()
{
	return  {
		edit:false,
		search:true,
		add: false,
		del:true
	};
}

// A link formatter.  Make sure to add a url in the format options formatoptions: { url: 'blah/blah', text: 'clickme'}
function linkFormatter(cellvalue, options, rowObject) {
	var url = options.colModel.formatoptions.url;
	var text = options.colModel.formatoptions.text;
	return '<a href="' + url + '/' + rowObject.Id + '">' + text + '</a>';
}