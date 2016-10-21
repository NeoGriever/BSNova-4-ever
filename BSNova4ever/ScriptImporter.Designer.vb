'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 12.10.2016
' Zeit: 20:00
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class ScriptImporter
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
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.button1 = New System.Windows.Forms.Button()
		Me.SuspendLayout
		'
		'textBox1
		'
		Me.textBox1.Location = New System.Drawing.Point(87, 12)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(204, 20)
		Me.textBox1.TabIndex = 0
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(12, 12)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(69, 20)
		Me.label1.TabIndex = 1
		Me.label1.Text = "Share-Code:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'button1
		'
		Me.button1.Location = New System.Drawing.Point(297, 11)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(44, 22)
		Me.button1.TabIndex = 2
		Me.button1.Text = "OK"
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'ScriptImporter
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(353, 43)
		Me.Controls.Add(Me.button1)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.textBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "ScriptImporter"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Script importieren"
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private button1 As System.Windows.Forms.Button
	Private label1 As System.Windows.Forms.Label
	Private textBox1 As System.Windows.Forms.TextBox
End Class
