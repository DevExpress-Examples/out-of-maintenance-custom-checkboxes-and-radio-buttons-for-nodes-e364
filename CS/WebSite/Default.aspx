<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="TreeList_Templates_ClientChecksAndRadios_Default" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Custom checkboxes and radio buttons for nodes</title>    
</head>
<body>
<script type="text/javascript">
function setHiddenState(nodeKey, isHidden) {
	var name = "mycheck_" + nodeKey;
	theForm.elements[name].value = isHidden ? "H" : "V";
}
</script>
<form id="form1" runat="server">

	<dxwtl:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False"
		DataSourceID="AccessDataSource1" KeyFieldName="ID" ParentFieldName="PARENTID"
		ClientInstanceName="tree" OnHtmlRowPrepared="ASPxTreeList1_HtmlRowPrepared">
		<SettingsLoadingPanel Delay="0" />
		<Columns>
			<dxwtl:TreeListDataColumn FieldName="DEPARTMENT" VisibleIndex="0"></dxwtl:TreeListDataColumn>
			<dxwtl:TreeListDataColumn Caption="Hide" VisibleIndex="1">
				<DataCellTemplate>					
					<input type="checkbox" <%# HiddenNodeList.Contains(Container.NodeKey) ? "checked=\"checked\"" : "" %> 
						onclick="setHiddenState('<%# Container.NodeKey %>', checked)" />
					<input type="hidden" name="mycheck_<%# Container.NodeKey %>" value="" />
				</DataCellTemplate>
			</dxwtl:TreeListDataColumn>
			<dxwtl:TreeListDataColumn Caption="Start" VisibleIndex="2">
				<DataCellTemplate>
					<input type="radio" name="myradio" value="<%# Container.NodeKey %>" <%# StartNodeKey == Container.NodeKey ? "checked=\"checked\"" : "" %> />
				</DataCellTemplate>
			</dxwtl:TreeListDataColumn>
		</Columns>
	</dxwtl:ASPxTreeList>
	
	<br />
	<asp:Button ID="Button1" runat="server" Text="Save" />
	
	<asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/Departments.mdb"
		SelectCommand="SELECT [ID], [ParentID], [Department] FROM [Departments]"></asp:AccessDataSource>
</form>
</body>
</html>
