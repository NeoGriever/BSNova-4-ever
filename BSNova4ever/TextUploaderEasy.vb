Imports System.IO
Imports System.Net
Imports System.Collections.Specialized
Imports System.Text

Public Class TextUploaderEasy
	Private Shared wcup As New WebClient()
	Private Shared Function ToBase64(ByVal sText As String) As String
		Dim nBytes() As Byte = System.Text.Encoding.Default.GetBytes(sText)
		Return(System.Convert.ToBase64String(nBytes))
	End Function
	Private Shared Function FromBase64(ByVal sText As String) As String
		Dim nBytes() As Byte = System.Convert.FromBase64String(sText)
		Return(System.Text.Encoding.Default.GetString(nBytes))
	End Function
	Public Shared Function Upload(ByVal stype As Integer,ByVal name As String,ByVal data As String,ByVal domains() As String) As String
		Dim pd As New NameValueCollection()
		pd.Add("timestamp",DateTime.Now.ToString())
		pd.Add("source",data)
		pd.Add("name",name)
		pd.Add("type",stype.ToString())
		Dim it As Integer = 0
		For Each i As String In domains
			pd.Add("domain[" & it & "]",i)
			it += 1
		Next
		Dim result As String = System.Text.Encoding.Default.GetString(wcup.UploadValues("http://postingroups.hol.es/s/source_post.php",pd))
		Dim xdoc As New System.Xml.XmlDocument()
		xdoc.LoadXml(result)
		Return(xdoc.ChildNodes(1).ChildNodes(0).InnerText)
	End Function
	Public Shared Function Download(ByVal shortlink As String) As System.Xml.XmlDocument
		Dim src As String = wcup.DownloadString("http://postingroups.hol.es/s/source_post.php?i=" & shortlink)
		If src <> "" Then
			Dim xdoc As New System.Xml.XmlDocument()
			xdoc.LoadXml(src)
			Return(xdoc)
		Else
			Return(Nothing)
		End If
	End Function
End Class