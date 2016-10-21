Public Partial Class HelpView
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	Sub HelpViewLoad(sender As Object, e As EventArgs)
		Dim src As String = System.IO.File.ReadAllText("pattern-help.html")
		WebBrowser1.Document.Write("<html><head></head><body></body></html>")
		WebBrowser1.Document.Body.InnerHtml = src
	End Sub
End Class
