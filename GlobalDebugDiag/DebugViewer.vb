Imports System.Windows.Forms
Imports System.IO
Imports System.Xml

Public Partial Class DebugViewer
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	Sub ListView1SizeChanged(sender As Object, e As EventArgs)
		columnHeader2.Width = listView1.Width - (columnHeader1.Width+columnHeader3.Width+System.Windows.Forms.SystemInformation.VerticalScrollBarWidth+10)
	End Sub
	
	Sub DebugViewerLoad(sender As Object, e As EventArgs)
		Dim x As New XmlDocument()
		x.Load("log.xml")
		Dim last As ListViewItem = Nothing
		Dim beginFrom As Integer = 0
		If x.ChildNodes(1).ChildNodes.Count > 100 Then
			beginFrom = x.ChildNodes(1).ChildNodes.Count - 100
		End If
		For numb As Integer = beginFrom To x.ChildNodes(1).ChildNodes.Count - 1
			Dim xnode As XmlNode = x.ChildNodes(1).ChildNodes(numb)
			Dim time_val As String = xNode.Attributes.GetNamedItem("time").Value
			Dim type_val As String = xNode.Attributes.GetNamedItem("type").Value
			Dim strn_val As String = CType(xNode.ChildNodes(0),XmlCDataSection).Data
			Dim i As New ListViewItem(time_val)
			i.SubItems.Add(strn_val)
			i.SubItems.Add(type_val)
			listView1.Items.Add(i)
			last = i
		Next
		If last IsNot Nothing Then
			last.EnsureVisible()
		End If
		AddHandler DebugDiag.MessageFired, AddressOf AddToList
	End Sub
	
	Private Delegate Sub AddToListD(ByVal msg As String,ByVal type As String,ByVal subcat As String)
	
	Sub AddToList(ByVal msg As String,ByVal type As String,ByVal subcat As String)
		If listView1.InvokeRequired Then
			listView1.Invoke(New AddToListD(AddressOf AddToList),New Object() {msg,type,subcat})
		Else
			Dim i As New ListViewItem(DateTime.Now.ToString())
			i.SubItems.Add(msg)
			i.SubItems.Add(type)
			listView1.Items.Add(i)
			i.EnsureVisible()
		End If
	End Sub
	
	Sub ListView1ItemActivate(sender As Object, e As EventArgs)
		Dim t As String = listView1.FocusedItem.Text
		Dim y As String = listView1.FocusedItem.SubItems(2).Text
		Dim d As String = listView1.FocusedItem.SubItems(1).Text
		Dim dwd As New DebugViewerDetails(t,y,d)
		dwd.ShowDialog()
	End Sub
	
	Sub LogdateiÜbermittelnToolStripMenuItemClick(sender As Object, e As EventArgs)
		Dim ulf As New UploadLogfile()
		ulf.ShowDialog()
	End Sub
End Class
