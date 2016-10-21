Public Partial Class DebugViewerDetails
	Public Sub New(ByVal time As String,ByVal type As String,ByVal data As String)
		Me.InitializeComponent()
		richTextBox1.Text = data
		label1.Text = time
		label2.Text = type
	End Sub
End Class
