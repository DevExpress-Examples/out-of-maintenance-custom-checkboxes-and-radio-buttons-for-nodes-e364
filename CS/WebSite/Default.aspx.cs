using System;
using System.Collections.Generic;
using DevExpress.Web.ASPxTreeList;

public partial class TreeList_Templates_ClientChecksAndRadios_Default : System.Web.UI.Page {
	const string HiddenListSessionKey = "UniqueString1";
	const string StartNodeSessionKey = "UniqueString2";

	protected override void OnLoad(EventArgs e) {
		base.OnLoad(e);
		if(!IsPostBack) {
			ASPxTreeList1.DataBind();
			ASPxTreeList1.ExpandToLevel(2);
		}
		if(IsPostBack)
			SaveState();
	}

	protected List<string> HiddenNodeList {
		get {
			List<string> list = Session[HiddenListSessionKey] as List<string>;
			if(list == null) {
				list = new List<string>();
				Session[HiddenListSessionKey] = list;
			}
			return list;
		}
	}
	protected string StartNodeKey {
		get {
			object value = Session[StartNodeSessionKey];
			if(value != null)
				return value.ToString();
			return null;
		}
		set {
			Session[StartNodeSessionKey] = value;
			ASPxTreeList1.DataBind();
		}
	}

	void SaveState() {
		const string prefix = "mycheck_";
		foreach(string key in Request.Params) {
			string value = Request.Params[key];
			if(key == "myradio")
				StartNodeKey = value;
			if(key.StartsWith(prefix) && !string.IsNullOrEmpty(value)) {
				string nodeKey = key.Substring(prefix.Length);
				if(value == "H")
					HiddenNodeList.Add(nodeKey);
				else if(value == "V")
					HiddenNodeList.Remove(nodeKey);
				ASPxTreeList1.DataBind();
			}

		}
	}

	protected void ASPxTreeList1_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e) {
		if(e.NodeKey == StartNodeKey)
			e.Row.BackColor = System.Drawing.Color.LightGreen;
		if(HiddenNodeList.Contains(e.NodeKey))
			e.Row.ForeColor = System.Drawing.Color.Silver;
	}
}
