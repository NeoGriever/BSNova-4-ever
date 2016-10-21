'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 03.10.2016
' Zeit: 18:02
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Public Partial Class ScriptingTester
	Public TestingSource As String = ""
	Public TestingTypes As Integer = 0
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		AddHandler QuickCompiler.ThrownError, AddressOf QQErr
		AddHandler GlobalDebugDiag.DebugDiag.MessageFired, AddressOf HandleLogEntries
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub QQErr(ByVal qerr As System.CodeDom.Compiler.CompilerError)
		AddToRTB(qerr.ErrorText,True)
		AddToRTB("Datei: " & qerr.FileName,True)
		AddToRTB("Zeile: " & qerr.Line,True)
		AddToRTB("Position: " & qerr.Column,True)
		AddToRTB("",True)
	End Sub
	
	Sub Button1Click(sender As Object, e As EventArgs)
		textBox1.Enabled = False
		button1.Enabled = False
		Dim scripTestThread As New System.Threading.Thread(AddressOf DoScriptTest)
		scripTestThread.Start()
	End Sub
	
	Delegate Sub ClearRTBD()
	Sub ClearRTB()
		If richTextBox1.InvokeRequired Then
			richTextBox1.Invoke(New ClearRTBD(AddressOf ClearRTB))
		Else
			richTextBox1.Clear()
		End If
	End Sub
	
	Delegate Sub AddToRTBD(ByVal s As String,ByVal br As Boolean)
	Sub AddToRTB(ByVal s As String,Optional ByVal br As Boolean = True)
		If richTextBox1.InvokeRequired Then
			richTextBox1.Invoke(New AddToRTBD(AddressOf AddToRTB),New Object() {s,br})
		Else
			richTextBox1.AppendText(s)
			If br Then
				richTextBox1.AppendText(System.Environment.NewLine)
			End If
		End If
	End Sub
	
	Delegate Sub FinItD()
	Sub FinIt()
		If button1.InvokeRequired Or textBox1.InvokeRequired Then
			Me.Invoke(New FinItD(AddressOf FinIt))
		Else
			textBox1.Enabled = True
			button1.Enabled = True
		End If
	End Sub
	
	Private Sub DoScriptTest()	
		ClearRTB()
		AddToRTB("Compiliere Code ...",False)
		Try
			Dim compiled_class As Object = QuickCompiler.Compile(TestingSource,TestingTypes)
			AddToRTB(" Fertig")
			AddToRTB("Teste Script ...")
			Dim DirectLink As String = compiled_class.ParseHoster(textBox1.Text).ToString()
			AddToRTB("Ergebnis: " & DirectLink)
		Catch ex As Exception
			AddToRTB(" Fehlschlag")
			AddToRTB(ex.ToString())
		End Try
		FinIt()
	End Sub
	Delegate Sub HandleLogEntriesD(ByVal msg As String,ByVal type As String,ByVal subcat As String)
	Sub HandleLogEntries(ByVal msg As String,ByVal type As String,ByVal subcat As String)
		If subcat = "TESTER" Then
			If statusStrip1.InvokeRequired Then
				statusStrip1.Invoke(New HandleLogEntriesD(AddressOf HandleLogEntries),New Object() {msg,type,subcat})
			Else
				AddToRTB(msg,True)
				toolStripStatusLabel1.Text = DateTime.Now.ToString() & " > " & msg
			End If
		End If
	End Sub
	
	Sub ToolStripStatusLabel1Click(sender As Object, e As EventArgs)
		GlobalDebugDiag.DebugDiag.ShowDebugView()
	End Sub
End Class
