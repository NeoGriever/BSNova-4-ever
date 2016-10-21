'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 12.10.2016
' Zeit: 19:46
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class ScriptSharer
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
		Me.label1 = New System.Windows.Forms.Label()
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.SuspendLayout
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(12, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(669, 17)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Dieser Code kann unter ""Importieren"" eingegeben werden, um das von dir geteilte S"& _ 
		"cript direkt zu jemand anderem zu übertragen."
		Me.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
		'
		'textBox1
		'
		Me.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.textBox1.Font = New System.Drawing.Font("Courier New", 35!)
		Me.textBox1.HideSelection = false
		Me.textBox1.Location = New System.Drawing.Point(12, 29)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.ReadOnly = true
		Me.textBox1.Size = New System.Drawing.Size(669, 60)
		Me.textBox1.TabIndex = 1
		Me.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		Me.textBox1.WordWrap = false
		'
		'ScriptSharer
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(693, 101)
		Me.Controls.Add(Me.textBox1)
		Me.Controls.Add(Me.label1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "ScriptSharer"
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Script teilen"
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private textBox1 As System.Windows.Forms.TextBox
	Private label1 As System.Windows.Forms.Label
End Class
