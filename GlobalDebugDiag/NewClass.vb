Imports System.Windows.Forms
Imports System.Text
Imports System.Collections.Specialized
Imports System.Xml

Public Class DebugDiag
	Public Shared Event MessageFired(ByVal msg As String,ByVal type As String,ByVal subcat As String)
	Public Shared x_file As XmlDocument = New XmlDocument()
	Private Shared CurrentTimestamp As String = ""
	Private Shared Sub Init(Optional ByVal forceInit As Boolean = False)
		If CurrentTimestamp = "" Or forceInit Then
			CurrentTimestamp = DateTime.Now.ToShortDateString() & "_" & DateTime.Now.ToLongTimeString()
			CurrentTimestamp = CurrentTimestamp.Replace(":","_")
		End If
		If Not System.IO.Directory.Exists("logs") Then
			System.IO.Directory.CreateDirectory("logs")
		End If
		If Not forceInit And System.IO.File.Exists("logs/" & CurrentTimestamp & ".xml") Then
			Try
				x_file.Load("logs/" & CurrentTimestamp & ".xml")
			Catch
				
				System.Threading.Thread.Sleep(10)
				Init(True)
			End Try
		Else
			Dim xmldecl As XmlDeclaration = x_file.CreateXmlDeclaration("1.0", Encoding.GetEncoding("ISO-8859-15").BodyName, "yes")
			x_file.AppendChild(xmldecl)
			Dim root As XmlElement = x_file.CreateElement("DebugLog")
			x_file.AppendChild(root)
			x_file.Save("logs/" & CurrentTimestamp & ".xml")
		End If
	End Sub
	Public Shared Overloads Sub Log(ByVal msg As String,ByVal type As String)
		Log(msg,type,"MAIN",True)
	End Sub
	Public Shared Overloads Sub Log(ByVal msg As String,ByVal type As String,ByVal visible As Boolean)
		Log(msg,type,"MAIN",visible)
	End Sub
	Public Shared Overloads Sub Log(ByVal msg As String,ByVal type As String,ByVal subcat As String)
		Log(msg,type,subcat,True)
	End Sub
	Public Shared Overloads Sub Log(ByVal msg As String,ByVal type As String,ByVal subcat As String,ByVal visible As Boolean)
		Init()
		Dim x_node As XmlElement = x_file.CreateElement("entry")
		Dim x_att As XmlAttribute = x_file.CreateAttribute("time")
		x_att.Value = DateTime.Now.ToString()
		x_node.Attributes.Append(x_att)
		x_att = x_file.CreateAttribute("type")
		x_att.Value = type
		x_node.Attributes.Append(x_att)
		x_att = x_file.CreateAttribute("sub")
		x_att.Value = subcat
		x_node.Attributes.Append(x_att)
		x_att = x_file.CreateAttribute("visible")
		If visible Then
			x_att.Value = "1"
		Else
			x_att.Value = "0"
		End If
		x_node.Attributes.Append(x_att)
		Dim x_data As XmlCDataSection = x_file.CreateCDataSection(msg)
		x_node.AppendChild(x_data)
		x_file.ChildNodes(1).AppendChild(x_node)
		Try
			x_file.Save("log.xml")
		Catch
		End Try
		System.Diagnostics.Debug.WriteLine(msg,type)
		RaiseEvent MessageFired(msg,type,subcat)
	End Sub
	Private Shared DebugViewStore As DebugViewer = Nothing
	Public Shared Sub ShowDebugView()
		Init()
		If DebugViewStore Is Nothing Then
			DebugViewStore = New DebugViewer()
			AddHandler DebugViewStore.Closed, AddressOf DVSClosed
			DebugViewStore.Show()
			DebugViewStore.BringToFront()
			DebugViewStore.Focus()
		Else
			DebugViewStore.BringToFront()
		End If
	End Sub
	Private Shared Sub DVSClosed(ByVal sender As Object,ByVal e As System.EventArgs)
		DebugViewStore.Dispose()
		DebugViewStore = Nothing
	End Sub
End Class
