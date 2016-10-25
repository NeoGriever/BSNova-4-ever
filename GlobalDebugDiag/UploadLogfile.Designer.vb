'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 22.10.2016
' Zeit: 19:38
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class UploadLogfile
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UploadLogfile))
		Me.label1 = New System.Windows.Forms.Label()
		Me.button1 = New System.Windows.Forms.Button()
		Me.button2 = New System.Windows.Forms.Button()
		Me.label2 = New System.Windows.Forms.Label()
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.label3 = New System.Windows.Forms.Label()
		Me.richTextBox1 = New System.Windows.Forms.RichTextBox()
		Me.SuspendLayout
		'
		'label1
		'
		Me.label1.BackColor = System.Drawing.Color.LemonChiffon
		Me.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.label1.Location = New System.Drawing.Point(12, 10)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(335, 149)
		Me.label1.TabIndex = 0
		Me.label1.Text = resources.GetString("label1.Text")
		'
		'button1
		'
		Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.button1.Location = New System.Drawing.Point(12, 165)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(182, 23)
		Me.button1.TabIndex = 3
		Me.button1.Text = "Abbrechen"
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'button2
		'
		Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button2.Enabled = false
		Me.button2.Location = New System.Drawing.Point(556, 165)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(182, 23)
		Me.button2.TabIndex = 4
		Me.button2.Text = "Logdatei jetzt Übertragen"
		Me.button2.UseVisualStyleBackColor = true
		AddHandler Me.button2.Click, AddressOf Me.Button2Click
		'
		'label2
		'
		Me.label2.Location = New System.Drawing.Point(353, 9)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(162, 17)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Dein Forenname (erforderlich):"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'textBox1
		'
		Me.textBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox1.Location = New System.Drawing.Point(521, 8)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(217, 20)
		Me.textBox1.TabIndex = 1
		AddHandler Me.textBox1.TextChanged, AddressOf Me.TextBox1TextChanged
		'
		'label3
		'
		Me.label3.Location = New System.Drawing.Point(353, 35)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(385, 17)
		Me.label3.TabIndex = 2
		Me.label3.Text = "Kurze Fehlerbeschreibung: (optional)"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'richTextBox1
		'
		Me.richTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.richTextBox1.DetectUrls = false
		Me.richTextBox1.HideSelection = false
		Me.richTextBox1.Location = New System.Drawing.Point(353, 55)
		Me.richTextBox1.Name = "richTextBox1"
		Me.richTextBox1.Size = New System.Drawing.Size(385, 104)
		Me.richTextBox1.TabIndex = 2
		Me.richTextBox1.Text = ""
		'
		'UploadLogfile
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(750, 200)
		Me.Controls.Add(Me.richTextBox1)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.textBox1)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.button2)
		Me.Controls.Add(Me.button1)
		Me.Controls.Add(Me.label1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "UploadLogfile"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.Text = "Logdatei zur Analyse hochladen ..."
		AddHandler FormClosing, AddressOf Me.UploadLogfileFormClosing
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private richTextBox1 As System.Windows.Forms.RichTextBox
	Private label3 As System.Windows.Forms.Label
	Private textBox1 As System.Windows.Forms.TextBox
	Private label2 As System.Windows.Forms.Label
	Private button2 As System.Windows.Forms.Button
	Private button1 As System.Windows.Forms.Button
	Private label1 As System.Windows.Forms.Label
End Class
