Public Partial Class UploadLogfile
	Public Sub New()
		Me.InitializeComponent()
		
	End Sub
	
	Sub TextBox1TextChanged(sender As Object, e As EventArgs)
		If textBox1.Text <> "" Then
			button2.Enabled = True
		Else
			button2.Enabled = False
		End If
	End Sub
	
	Sub Button1Click(sender As Object, e As EventArgs)
		Me.Close()
	End Sub
	
	Private allowClose As Boolean = True
	
	Sub Button2Click(sender As Object, e As EventArgs)
		button1.Enabled = False
		button2.Enabled = False
		textBox1.Enabled = False
		richTextBox1.Enabled = False
		allowClose = False
		Dim t As New System.Threading.Thread(AddressOf WorkUpload)
		t.Start()
	End Sub
	Private wc As New System.Net.WebClient()
	Private Sub WorkUpload()
		Dim data As List(Of String) = GetData()
		
		Dim nvc As New System.Collections.Specialized.NameValueCollection()
		nvc.Add("user",data.Item(0))
		nvc.Add("desc",data.Item(1))
		nvc.Add("data",data.Item(2))
		
		Try
			Dim result As String = System.Text.Encoding.Default.GetString(wc.UploadValues("http://postingroups.hol.es/log_post.php",nvc))
			System.Windows.Forms.MessageBox.Show(result)
		Catch ex As Exception
		End Try
		
		UploadItDone()
	End Sub
	
	Private Delegate Function GetDataD() As List(Of String)
	Private Function GetData() As List(Of String)
		If Me.InvokeRequired Then
			Return(CType(Me.Invoke(New GetDataD(AddressOf GetData)),List(Of String)))
		Else
			Dim description As String = richTextBox1.Text
			Dim username As String = textBox1.Text
			Dim files() As String = System.IO.Directory.GetFiles("logs","*_*.xml")
			Dim src_logfile As String = System.IO.File.ReadAllText(files(files.Length - 1))
			Dim los As New List(Of String)
			los.Add(username)
			los.Add(description)
			los.Add(src_logfile)
			Return(los)
		End If
	End Function
	
	Private Delegate Sub UploadItDoneD()
	Private Sub UploadItDone()
		If Me.InvokeRequired Then
			Me.Invoke(New UploadItDoneD(AddressOf UploadItDone))
		Else
			allowClose = True
			Me.Close()
		End If
	End Sub
	
	Sub UploadLogfileFormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs)
		e.Cancel = Not allowClose
	End Sub
End Class
