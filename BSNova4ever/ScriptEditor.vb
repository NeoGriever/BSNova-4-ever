Public Partial Class ScriptEditor
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	Sub ScriptEditorLoad(sender As Object, e As EventArgs)
		For Each i As String In HosterScripts.GetPriorityList()
			comboBox1.Items.Add(i)
		Next
		If comboBox1.Items.Count > 0 Then
			comboBox1.SelectedIndex = 0
		End If
		scriptTypeSelection.SelectedIndex = 0
	End Sub
	
	Sub ComboBox1SelectedIndexChanged(sender As Object, e As EventArgs)
		button2.Enabled = True
		button5.Enabled = True
	End Sub
	
	Sub Button1Click(sender As Object, e As EventArgs)
		Dim ulist As New List(Of Uri)
		For Each i As String In textBox2.Text.Trim().Split(CChar(","))
			Try
				If i.Trim().IndexOf("http") < 0 Then
					i = "http://" & i
				End If
				ulist.Add(New Uri(i.Trim()))
			Catch
			End Try
		Next
		HosterScripts.SaveHosterScript(textBox1.Text,ulist,richTextBox1.Text,scriptTypeSelection.SelectedIndex)
		Dim n As New List(Of String)
		For Each i As String In comboBox1.Items
			n.Add(i)
		Next
		If n.IndexOf(textBox1.Text) < 0 Then
			comboBox1.Items.Add(textBox1.Text)
			comboBox1.SelectedText = textBox1.Text
		End If
	End Sub
	
	Sub Button2Click(sender As Object, e As EventArgs)
		Try
			Dim src As String = HosterScripts.GetHosterScript(comboBox1.Text)
			richTextBox1.Text = src
			textBox1.Text = comboBox1.Text
			textBox2.Text = String.Join(",",HosterScripts.GetHosterDomains(comboBox1.Text))
			scriptTypeSelection.SelectedIndex = HosterScripts.GetHosterScriptType(comboBox1.Text)
			button1.Enabled = True
		Catch ex As Exception
			MessageBox.Show(ex.ToString())
		End Try
	End Sub
	
	Sub Button3Click(sender As Object, e As EventArgs)
		Dim br As String = System.Environment.NewLine
		Dim src As String = ""
		If scriptTypeSelection.SelectedIndex = 0 Then
			src = "Imports System.IO" & br & _
			"Imports System.Net" & br & _
			"Imports System.Collections.Specialized" & br & _
			"" & br & _
			"Public Class HosterParser" & br & _
			"	Private Shared networkHandler As New WebClient()" & br & _
			"	Public Shared Function ParseHoster(ByVal hosterlink As String) As String" & br & _
			"		Dim src As String = networkHandler.DownloadString(hosterlink)" & br & _
			"		' " & br & _
			"		' Do here the magic stuff" & br & _
			"		' " & br & _
			"		' Log-Entries do like this:" & br & _
			"		' GlobalDebugDiag.DebugDiag.Log(""YourMessage"",""Information"",""TESTER"")" & br & _
			"		' " & br & _
			"		' Keep ""TESTER"". ""Information"" is free to change. Defines the type of the log-entry. Like ""Error"", ""Debug"", ""Source"" etc." & br & _
			"		' " & br & _
			"		Return("""")" & br & _
			"	End Function" & br & _
			"End Class"
		Else
			src = "using Microsoft.VisualBasic;" & br & _
				"using System;" & br & _
				"using System.Collections;" & br & _
				"using System.Collections.Generic;" & br & _
				"using System.Diagnostics;" & br & _
				"using System.Drawing;" & br & _
				"using System.Windows.Forms;" & br & _
				"using System.IO;" & br & _
				"using System.Net;" & br & _
				"using System.Collections.Specialized;" & br & _
				"" & br & _
				"public class HosterParser" & br & _
				"{" & br & _
				"	private static WebClient networkHandler = new WebClient();" & br & _
				"	public static string ParseHoster(string hosterlink)" & br & _
				"	{" & br & _
				"		string src = networkHandler.DownloadString(hosterlink);" & br & _
				"		// " & br & _
				"		// Do here the magic stuff" & br & _
				"		// " & br & _
				"		// Log-Entries do like this:" & br & _
				"		// GlobalDebugDiag.DebugDiag.Log(""YourMessage"",""Information"",""TESTER"");" & br & _
				"		// " & br & _
				"		// Keep ""TESTER"". ""Information"" is free to change. Defines the type of the log-entry. Like ""Error"", ""Debug"", ""Source"" etc." & br & _
				"		// " & br & _
				"		return ("""");" & br & _
				"	}" & br & _
				"}"
		End If
		If richTextBox1.Text <> "" Then
			If MessageBox.Show("Möchtest du den vorhandenen Code mit der Scriptvorlage ersetzen?","Ersetzen?",MessageBoxButtons.YesNo,MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
				richTextBox1.Text = src
			End If
		Else
			richTextBox1.Text = src
		End If
	End Sub
	
	Sub Button4Click(sender As Object, e As EventArgs)
		Dim testScript As New ScriptingTester()
		testScript.TestingSource = richTextBox1.Text
		testScript.TestingTypes = scriptTypeSelection.SelectedIndex
		testScript.ShowDialog()
	End Sub
	
	Sub delScriptNow(sender As Object, e As EventArgs)
		If MessageBox.Show("Möchten Sie das ausgewählte Parsing-Script wirklich löschen?","Löschen bestätigen!",MessageBoxButtons.YesNo,MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
			Dim curItem As Object = comboBox1.SelectedItem
			HosterScripts.RemoveHosterScript(comboBox1.Text)
			comboBox1.Items.Remove(curItem)
			comboBox1.SelectedIndex = 0
		End If
	End Sub
	
	Sub RichTextBox1TextChanged(sender As Object, e As EventArgs)
		timer1.Stop()
		timer1.Enabled = False
		timer1.Enabled = True
		timer1.Start()
	End Sub
	
	Sub Timer1Tick(sender As Object, e As EventArgs)
		If toolStripStatusLabel1.Visible <> True And toolStripStatusLabel5.Visible Then
			timer1.Stop()
			timer1.Enabled = False
			SetB(New Boolean() {True,False,False,False,False,False})
			Dim t As New System.Threading.Thread(AddressOf BackgroundValidateThread)
			t.Start()
		End If
	End Sub
	
	Private Delegate Function GetSrc() As String
	Private Function GetS() As String
		If richTextBox1.InvokeRequired Then
			Return(CStr(richTextBox1.Invoke(New GetSrc(AddressOf GetS))))
		Else
			Return(richTextBox1.Text)
		End If
	End Function
	
	Private Delegate Sub SetBtn(ByVal bools As Boolean())
	Private Sub SetB(ByVal bools As Boolean())
		If StatusStrip1.InvokeRequired Then
			statusStrip1.Invoke(New SetBtn(AddressOf SetB),New Object() {bools})
		Else
			toolStripStatusLabel1.Visible = bools(0)
			toolStripStatusLabel2.Visible = bools(1)
			toolStripStatusLabel3.Visible = bools(2)
			toolStripStatusLabel4.Visible = bools(3)
			toolStripStatusLabel5.Visible = bools(4)
			toolStripStatusLabel6.Visible = bools(5)
		End If
	End Sub
	
	Private _temp_error_messages As New List(Of String)
	
	Private Sub BesideSaveErrors(ByVal cerr As System.CodeDom.Compiler.CompilerError)
		_temp_error_messages.Add(cerr.ToString())
	End Sub
	
	Private Delegate Function getSelTypD() As Integer
	Private Function getSelTyp() As Integer
		If scriptTypeSelection.InvokeRequired Then
			Return(CInt(scriptTypeSelection.Invoke(New getSelTypD(AddressOf getSelTyp))))
		Else
			Return(scriptTypeSelection.SelectedIndex)
		End If
	End Function
	
	Private Sub BackgroundValidateThread()
		Dim src As String = GetS()
		_temp_error_messages = New List(Of String)
		AddHandler QuickCompiler.ThrownError, AddressOf BesideSaveErrors
		Try
			Dim classHandle As Object = QuickCompiler.Compile(src,getSelTyp())
			Try
				Dim f As String = CStr(classHandle.ParseHoster(""))
				GlobalDebugDiag.DebugDiag.Log("Schnelltest erfolgreich","Information")
				SetB(New Boolean() {False,False,True,False,True,False})
			Catch ex As Exception
				If ex.Message.Trim() = "Public member 'ParseHoster' on type 'HosterParser' not found." Then
					GlobalDebugDiag.DebugDiag.Log("Schnelltest fehlgeschlagen. Funktoinsname falsch!","Fehler")
					SetB(New Boolean() {False,True,False,False,True,False})
				ElseIf ex.Message.Trim() = "Object variable or With block variable not set." Then
					GlobalDebugDiag.DebugDiag.Log("Schnelltest fehlgeschlagen. Prüfe den Klassennamen!","Fehler")
					SetB(New Boolean() {False,True,False,False,True,False})
				Else
					If _temp_error_messages.Count = 0 Then
						GlobalDebugDiag.DebugDiag.Log("Schnelltest erfolgreich","Information")
						SetB(New Boolean() {False,False,True,False,True,False})
					Else
						GlobalDebugDiag.DebugDiag.Log("Schnelltest fehlgeschlagen","Fehler")
						For Each i As String In _temp_error_messages
							GlobalDebugDiag.DebugDiag.Log(i,"Fehler")
						Next
						SetB(New Boolean() {False,True,False,False,True,False})
					End If
				End If
			End Try
		Catch ex As Exception
			GlobalDebugDiag.DebugDiag.Log("Schnelltest fehlgeschlagen","Fehler")
			For Each i As String In _temp_error_messages
				GlobalDebugDiag.DebugDiag.Log(i,"Fehler")
			Next
			SetB(New Boolean() {False,True,False,False,True,False})
		End Try
		RemoveHandler QuickCompiler.ThrownError, AddressOf BesideSaveErrors
	End Sub
	
	Sub ToolStripStatusLabel1Click(sender As Object, e As EventArgs)
		timer1Tick(sender,e)
	End Sub
	
	Sub ToolStripStatusLabel5Click(sender As Object, e As EventArgs)
		toolStripStatusLabel5.Visible = False
		toolStripStatusLabel6.Visible = True
		toolStripStatusLabel1.Visible = False
		toolStripStatusLabel2.Visible = False
		toolStripStatusLabel3.Visible = False
		toolStripStatusLabel4.Visible = False
	End Sub
	
	Sub ToolStripStatusLabel6Click(sender As Object, e As EventArgs)
		toolStripStatusLabel5.Visible = True
		toolStripStatusLabel6.Visible = False
		toolStripStatusLabel1.Visible = False
		toolStripStatusLabel2.Visible = False
		toolStripStatusLabel3.Visible = False
		toolStripStatusLabel4.Visible = True
	End Sub
	
	Sub Button6Click(sender As Object, e As EventArgs)
		Dim sshare As New ScriptSharer(richTextBox1.Text,textBox1.Text,textBox2.Text,scriptTypeSelection.SelectedIndex)
		If Not sshare.IsDisposed Then
			sshare.ShowDialog()
		End If
	End Sub
	
	Sub Button7Click(sender As Object, e As EventArgs)
		Dim loader As New ScriptImporter()
		loader.ShowDialog()
		If loader.Valid Then
			textBox1.Text = loader.Name
			textBox2.Text = loader.Domains
			richTextBox1.Text = loader.Source
			scriptTypeSelection.SelectedIndex = loader.sType
		End If
	End Sub
End Class
