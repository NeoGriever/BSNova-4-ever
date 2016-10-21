Imports System.Xml

Public Partial Class ScriptImporter
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	Public Valid As Boolean = False
	Public Shadows Name As String = ""
	Public Domains As String = ""
	Public Source As String = ""
	Public sType As Integer = 0
	Sub Button1Click(sender As Object, e As EventArgs)
		textBox1.Enabled = False
		button1.Enabled = False
		Application.DoEvents()
		Dim x As XmlDocument = TextUploaderEasy.Download(textBox1.Text)
		If x Is Nothing Then
			MessageBox.Show("Der eingegebene Share-Code ist ungültig. Bitte prüfe diesen und versuche es erneut.","Fehler",MessageBoxButtons.OK,MessageBoxIcon.Error)
			textBox1.Enabled = True
			button1.Enabled = True
			textBox1.Focus()
			textBox1.Select(0,textBox1.Text.Length)
		Else
			Valid = True
			Name = x.ChildNodes(1).ChildNodes(0).InnerText
			Source = x.ChildNodes(1).ChildNodes(1).ChildNodes(0).Value
			sType = CInt(x.ChildNodes(1).ChildNodes(2).InnerText)
			Dim tempList As New List(Of String)
			For Each d As XmlNode In x.ChildNodes(1).ChildNodes(3).ChildNodes
				tempList.Add(d.InnerText)
			Next
			Domains = String.Join(",",tempList.ToArray())
			Me.Close()
		End If
	End Sub
End Class
