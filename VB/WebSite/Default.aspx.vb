Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports DevExpress.Web.ASPxTreeList

Partial Public Class TreeList_Templates_ClientChecksAndRadios_Default
	Inherits System.Web.UI.Page
	Private Const HiddenListSessionKey As String = "UniqueString1"
	Private Const StartNodeSessionKey As String = "UniqueString2"

	Protected Overrides Sub OnLoad(ByVal e As EventArgs)
		MyBase.OnLoad(e)
		If (Not IsPostBack) Then
			ASPxTreeList1.DataBind()
			ASPxTreeList1.ExpandToLevel(2)
		End If
		If IsPostBack Then
			SaveState()
		End If
	End Sub

	Protected ReadOnly Property HiddenNodeList() As List(Of String)
		Get
			Dim list As List(Of String) = TryCast(Session(HiddenListSessionKey), List(Of String))
			If list Is Nothing Then
				list = New List(Of String)()
				Session(HiddenListSessionKey) = list
			End If
			Return list
		End Get
	End Property
	Protected Property StartNodeKey() As String
		Get
			Dim value As Object = Session(StartNodeSessionKey)
			If value IsNot Nothing Then
				Return value.ToString()
			End If
			Return Nothing
		End Get
		Set(ByVal value As String)
			Session(StartNodeSessionKey) = value
			ASPxTreeList1.DataBind()
		End Set
	End Property

	Private Sub SaveState()
		Const prefix As String = "mycheck_"
		For Each key As String In Request.Params
			Dim value As String = Request.Params(key)
			If key = "myradio" Then
				StartNodeKey = value
			End If
			If key.StartsWith(prefix) AndAlso (Not String.IsNullOrEmpty(value)) Then
				Dim nodeKey As String = key.Substring(prefix.Length)
				If value = "H" Then
					HiddenNodeList.Add(nodeKey)
				ElseIf value = "V" Then
					HiddenNodeList.Remove(nodeKey)
				End If
				ASPxTreeList1.DataBind()
			End If

		Next key
	End Sub

	Protected Sub ASPxTreeList1_HtmlRowPrepared(ByVal sender As Object, ByVal e As TreeListHtmlRowEventArgs)
		If e.NodeKey = StartNodeKey Then
			e.Row.BackColor = System.Drawing.Color.LightGreen
		End If
		If HiddenNodeList.Contains(e.NodeKey) Then
			e.Row.ForeColor = System.Drawing.Color.Silver
		End If
	End Sub
End Class
