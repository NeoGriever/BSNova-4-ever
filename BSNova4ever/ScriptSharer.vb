Public Partial Class ScriptSharer
	Public Sub New(ByVal src As String,ByVal name As String,ByVal domaintxt As String,ByVal stype As Integer)
		If name <> "" Then
			If domaintxt <> "" Then
				If src <> "" Then
					Me.InitializeComponent()
					Dim dmns As New List(Of String)
					If domaintxt.IndexOf(",") > -1 Then
						For Each i As String In domaintxt.Split(CChar(","))
							Dim l As String = i
							If l.IndexOf("http") <> 0 Then
								l = "http://" & l
							End If
							Dim h As New Uri(l)
							dmns.Add(h.Host)
						Next
					Else
						Dim l As String = domaintxt
						If l.IndexOf("http") <> 0 Then
							l = "http://" & l
						End If
						Dim h As New Uri(l)
						dmns.Add(h.Host)
					End If
					Dim shorttag As String = TextUploaderEasy.Upload(stype,name,src,dmns.ToArray())
					textBox1.Text = shorttag
				Else
					MessageBox.Show("Der Quelltext darf nicht leer sein!","Fehler",MessageBoxButtons.OK,MessageBoxIcon.Error)
					Me.Close()
				End If
			Else
				MessageBox.Show("Es muss mindestens eine gültige Domain angegeben werden!","Fehler",MessageBoxButtons.OK,MessageBoxIcon.Error)
				Me.Close()
			End If
		Else
			MessageBox.Show("Es muss ein Hoster-Name angegeben werden!","Fehler",MessageBoxButtons.OK,MessageBoxIcon.Error)
			Me.Close()
		End If
	End Sub
End Class
