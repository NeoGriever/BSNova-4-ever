'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 03.10.2016
' Zeit: 18:02
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class ScriptingTester
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScriptingTester))
		Me.richTextBox1 = New System.Windows.Forms.RichTextBox()
		Me.panel1 = New System.Windows.Forms.Panel()
		Me.label1 = New System.Windows.Forms.Label()
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.button1 = New System.Windows.Forms.Button()
		Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.panel1.SuspendLayout
		Me.statusStrip1.SuspendLayout
		Me.SuspendLayout
		'
		'richTextBox1
		'
		Me.richTextBox1.DetectUrls = false
		Me.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.richTextBox1.HideSelection = false
		Me.richTextBox1.Location = New System.Drawing.Point(0, 27)
		Me.richTextBox1.Name = "richTextBox1"
		Me.richTextBox1.ReadOnly = true
		Me.richTextBox1.Size = New System.Drawing.Size(867, 284)
		Me.richTextBox1.TabIndex = 2
		Me.richTextBox1.Text = ""
		Me.richTextBox1.WordWrap = false
		'
		'panel1
		'
		Me.panel1.Controls.Add(Me.label1)
		Me.panel1.Controls.Add(Me.textBox1)
		Me.panel1.Controls.Add(Me.button1)
		Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.panel1.Location = New System.Drawing.Point(0, 0)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(867, 27)
		Me.panel1.TabIndex = 4
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(0, 6)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(70, 20)
		Me.label1.TabIndex = 3
		Me.label1.Text = "Hoster-Link"
		'
		'textBox1
		'
		Me.textBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox1.Location = New System.Drawing.Point(76, 3)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(689, 20)
		Me.textBox1.TabIndex = 0
		'
		'button1
		'
		Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button1.Location = New System.Drawing.Point(771, 1)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(96, 22)
		Me.button1.TabIndex = 1
		Me.button1.Text = "Test starten"
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'statusStrip1
		'
		Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabel1})
		Me.statusStrip1.Location = New System.Drawing.Point(0, 311)
		Me.statusStrip1.Name = "statusStrip1"
		Me.statusStrip1.Size = New System.Drawing.Size(867, 22)
		Me.statusStrip1.TabIndex = 5
		Me.statusStrip1.Text = "statusStrip1"
		'
		'toolStripStatusLabel1
		'
		Me.toolStripStatusLabel1.AutoSize = false
		Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
		Me.toolStripStatusLabel1.Size = New System.Drawing.Size(852, 17)
		Me.toolStripStatusLabel1.Spring = true
		Me.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		AddHandler Me.toolStripStatusLabel1.Click, AddressOf Me.ToolStripStatusLabel1Click
		'
		'ScriptingTester
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(867, 333)
		Me.Controls.Add(Me.richTextBox1)
		Me.Controls.Add(Me.statusStrip1)
		Me.Controls.Add(Me.panel1)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.Name = "ScriptingTester"
		Me.Text = "ScriptingTester"
		Me.panel1.ResumeLayout(false)
		Me.panel1.PerformLayout
		Me.statusStrip1.ResumeLayout(false)
		Me.statusStrip1.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
	Private statusStrip1 As System.Windows.Forms.StatusStrip
	Private panel1 As System.Windows.Forms.Panel
	Private label1 As System.Windows.Forms.Label
	Private richTextBox1 As System.Windows.Forms.RichTextBox
	Private button1 As System.Windows.Forms.Button
	Private textBox1 As System.Windows.Forms.TextBox
End Class
