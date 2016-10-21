'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 11.10.2016
' Zeit: 16:09
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class HelpView
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.webBrowser1 = New System.Windows.Forms.WebBrowser()
		Me.SuspendLayout
		'
		'webBrowser1
		'
		Me.webBrowser1.AllowNavigation = false
		Me.webBrowser1.AllowWebBrowserDrop = false
		Me.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.webBrowser1.Location = New System.Drawing.Point(0, 0)
		Me.webBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
		Me.webBrowser1.Name = "webBrowser1"
		Me.webBrowser1.ScriptErrorsSuppressed = true
		Me.webBrowser1.Size = New System.Drawing.Size(725, 504)
		Me.webBrowser1.TabIndex = 0
		Me.webBrowser1.Url = New System.Uri("", System.UriKind.Relative)
		'
		'HelpView
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(725, 504)
		Me.Controls.Add(Me.webBrowser1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "HelpView"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Pattern-Hilfe"
		AddHandler Load, AddressOf Me.HelpViewLoad
		Me.ResumeLayout(false)
	End Sub
	Private webBrowser1 As System.Windows.Forms.WebBrowser
End Class
