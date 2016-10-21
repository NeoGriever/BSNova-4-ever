'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 22.09.2016
' Zeit: 04:58
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class ScriptEditor
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
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScriptEditor))
		Me.comboBox1 = New System.Windows.Forms.ComboBox()
		Me.button1 = New System.Windows.Forms.Button()
		Me.button2 = New System.Windows.Forms.Button()
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.richTextBox1 = New System.Windows.Forms.RichTextBox()
		Me.textBox2 = New System.Windows.Forms.TextBox()
		Me.button3 = New System.Windows.Forms.Button()
		Me.button4 = New System.Windows.Forms.Button()
		Me.scriptTypeSelection = New System.Windows.Forms.ComboBox()
		Me.button5 = New System.Windows.Forms.Button()
		Me.timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.toolStripStatusLabel7 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.toolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.toolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.toolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.toolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.toolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.label1 = New System.Windows.Forms.Label()
		Me.label2 = New System.Windows.Forms.Label()
		Me.label3 = New System.Windows.Forms.Label()
		Me.label4 = New System.Windows.Forms.Label()
		Me.button6 = New System.Windows.Forms.Button()
		Me.button7 = New System.Windows.Forms.Button()
		Me.statusStrip1.SuspendLayout
		Me.SuspendLayout
		'
		'comboBox1
		'
		Me.comboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.comboBox1.FormattingEnabled = true
		Me.comboBox1.Location = New System.Drawing.Point(125, 12)
		Me.comboBox1.Name = "comboBox1"
		Me.comboBox1.Size = New System.Drawing.Size(554, 21)
		Me.comboBox1.TabIndex = 0
		AddHandler Me.comboBox1.SelectedIndexChanged, AddressOf Me.ComboBox1SelectedIndexChanged
		'
		'button1
		'
		Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button1.Location = New System.Drawing.Point(818, 41)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(95, 25)
		Me.button1.TabIndex = 8
		Me.button1.Text = "Script speichern"
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'button2
		'
		Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button2.Enabled = false
		Me.button2.Location = New System.Drawing.Point(685, 12)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(127, 23)
		Me.button2.TabIndex = 1
		Me.button2.Text = "Script laden"
		Me.button2.UseVisualStyleBackColor = true
		AddHandler Me.button2.Click, AddressOf Me.Button2Click
		'
		'textBox1
		'
		Me.textBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox1.Location = New System.Drawing.Point(95, 43)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(494, 20)
		Me.textBox1.TabIndex = 2
		'
		'richTextBox1
		'
		Me.richTextBox1.AcceptsTab = true
		Me.richTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.richTextBox1.DetectUrls = false
		Me.richTextBox1.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.richTextBox1.HideSelection = false
		Me.richTextBox1.Location = New System.Drawing.Point(12, 98)
		Me.richTextBox1.Name = "richTextBox1"
		Me.richTextBox1.Size = New System.Drawing.Size(901, 348)
		Me.richTextBox1.TabIndex = 6
		Me.richTextBox1.Text = ""
		Me.richTextBox1.WordWrap = false
		AddHandler Me.richTextBox1.TextChanged, AddressOf Me.RichTextBox1TextChanged
		'
		'textBox2
		'
		Me.textBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox2.Location = New System.Drawing.Point(95, 72)
		Me.textBox2.Name = "textBox2"
		Me.textBox2.Size = New System.Drawing.Size(494, 20)
		Me.textBox2.TabIndex = 3
		'
		'button3
		'
		Me.button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button3.Location = New System.Drawing.Point(595, 71)
		Me.button3.Name = "button3"
		Me.button3.Size = New System.Drawing.Size(84, 22)
		Me.button3.TabIndex = 5
		Me.button3.Text = "Grundscript"
		Me.button3.UseVisualStyleBackColor = true
		AddHandler Me.button3.Click, AddressOf Me.Button3Click
		'
		'button4
		'
		Me.button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button4.Location = New System.Drawing.Point(685, 71)
		Me.button4.Name = "button4"
		Me.button4.Size = New System.Drawing.Size(61, 22)
		Me.button4.TabIndex = 7
		Me.button4.Text = "Testen ..."
		Me.button4.UseVisualStyleBackColor = true
		AddHandler Me.button4.Click, AddressOf Me.Button4Click
		'
		'scriptTypeSelection
		'
		Me.scriptTypeSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.scriptTypeSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.scriptTypeSelection.FormattingEnabled = true
		Me.scriptTypeSelection.Items.AddRange(New Object() {"VB.NET", "C#"})
		Me.scriptTypeSelection.Location = New System.Drawing.Point(685, 43)
		Me.scriptTypeSelection.Name = "scriptTypeSelection"
		Me.scriptTypeSelection.Size = New System.Drawing.Size(127, 21)
		Me.scriptTypeSelection.TabIndex = 4
		'
		'button5
		'
		Me.button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button5.Enabled = false
		Me.button5.Location = New System.Drawing.Point(819, 12)
		Me.button5.Name = "button5"
		Me.button5.Size = New System.Drawing.Size(94, 23)
		Me.button5.TabIndex = 999
		Me.button5.TabStop = false
		Me.button5.Text = "Script löschen"
		Me.button5.UseVisualStyleBackColor = true
		AddHandler Me.button5.Click, AddressOf Me.delScriptNow
		'
		'timer1
		'
		Me.timer1.Interval = 2000
		AddHandler Me.timer1.Tick, AddressOf Me.Timer1Tick
		'
		'statusStrip1
		'
		Me.statusStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
		Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabel7, Me.toolStripStatusLabel5, Me.toolStripStatusLabel6, Me.toolStripStatusLabel4, Me.toolStripStatusLabel1, Me.toolStripStatusLabel3, Me.toolStripStatusLabel2})
		Me.statusStrip1.Location = New System.Drawing.Point(0, 455)
		Me.statusStrip1.Name = "statusStrip1"
		Me.statusStrip1.Size = New System.Drawing.Size(925, 29)
		Me.statusStrip1.TabIndex = 8
		Me.statusStrip1.Text = "statusStrip1"
		'
		'toolStripStatusLabel7
		'
		Me.toolStripStatusLabel7.Name = "toolStripStatusLabel7"
		Me.toolStripStatusLabel7.Size = New System.Drawing.Size(73, 24)
		Me.toolStripStatusLabel7.Text = "Validierung: "
		'
		'toolStripStatusLabel5
		'
		Me.toolStripStatusLabel5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.toolStripStatusLabel5.Image = CType(resources.GetObject("toolStripStatusLabel5.Image"),System.Drawing.Image)
		Me.toolStripStatusLabel5.Name = "toolStripStatusLabel5"
		Me.toolStripStatusLabel5.Size = New System.Drawing.Size(24, 24)
		Me.toolStripStatusLabel5.ToolTipText = "Auto-Schnelltest ist an"
		AddHandler Me.toolStripStatusLabel5.Click, AddressOf Me.ToolStripStatusLabel5Click
		'
		'toolStripStatusLabel6
		'
		Me.toolStripStatusLabel6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.toolStripStatusLabel6.Image = CType(resources.GetObject("toolStripStatusLabel6.Image"),System.Drawing.Image)
		Me.toolStripStatusLabel6.Name = "toolStripStatusLabel6"
		Me.toolStripStatusLabel6.Size = New System.Drawing.Size(24, 24)
		Me.toolStripStatusLabel6.ToolTipText = "Auto-Schnelltest ist aus"
		Me.toolStripStatusLabel6.Visible = false
		AddHandler Me.toolStripStatusLabel6.Click, AddressOf Me.ToolStripStatusLabel6Click
		'
		'toolStripStatusLabel4
		'
		Me.toolStripStatusLabel4.Image = CType(resources.GetObject("toolStripStatusLabel4.Image"),System.Drawing.Image)
		Me.toolStripStatusLabel4.Name = "toolStripStatusLabel4"
		Me.toolStripStatusLabel4.Size = New System.Drawing.Size(24, 24)
		Me.toolStripStatusLabel4.ToolTipText = "Schnelltest - Start"
		AddHandler Me.toolStripStatusLabel4.Click, AddressOf Me.ToolStripStatusLabel1Click
		'
		'toolStripStatusLabel1
		'
		Me.toolStripStatusLabel1.Image = CType(resources.GetObject("toolStripStatusLabel1.Image"),System.Drawing.Image)
		Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
		Me.toolStripStatusLabel1.Size = New System.Drawing.Size(24, 24)
		Me.toolStripStatusLabel1.ToolTipText = "Schnelltest - Aktiv"
		Me.toolStripStatusLabel1.Visible = false
		AddHandler Me.toolStripStatusLabel1.Click, AddressOf Me.ToolStripStatusLabel1Click
		'
		'toolStripStatusLabel3
		'
		Me.toolStripStatusLabel3.Image = CType(resources.GetObject("toolStripStatusLabel3.Image"),System.Drawing.Image)
		Me.toolStripStatusLabel3.Name = "toolStripStatusLabel3"
		Me.toolStripStatusLabel3.Size = New System.Drawing.Size(24, 24)
		Me.toolStripStatusLabel3.ToolTipText = "Schnelltest - Erfolgreich"
		Me.toolStripStatusLabel3.Visible = false
		AddHandler Me.toolStripStatusLabel3.Click, AddressOf Me.ToolStripStatusLabel1Click
		'
		'toolStripStatusLabel2
		'
		Me.toolStripStatusLabel2.Image = CType(resources.GetObject("toolStripStatusLabel2.Image"),System.Drawing.Image)
		Me.toolStripStatusLabel2.Name = "toolStripStatusLabel2"
		Me.toolStripStatusLabel2.Size = New System.Drawing.Size(24, 24)
		Me.toolStripStatusLabel2.ToolTipText = "Schnelltest - Fehlgeschlagen"
		Me.toolStripStatusLabel2.Visible = false
		AddHandler Me.toolStripStatusLabel2.Click, AddressOf Me.ToolStripStatusLabel1Click
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(12, 43)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(77, 21)
		Me.label1.TabIndex = 9
		Me.label1.Text = "Hoster-Name:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label2
		'
		Me.label2.Location = New System.Drawing.Point(12, 72)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(77, 20)
		Me.label2.TabIndex = 10
		Me.label2.Text = "Domains:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label3
		'
		Me.label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label3.Location = New System.Drawing.Point(595, 43)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(84, 20)
		Me.label3.TabIndex = 11
		Me.label3.Text = "Script-Sprache:"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label4
		'
		Me.label4.Location = New System.Drawing.Point(12, 12)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(107, 21)
		Me.label4.TabIndex = 12
		Me.label4.Text = "Vorhandene Scripts:"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'button6
		'
		Me.button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button6.Location = New System.Drawing.Point(752, 71)
		Me.button6.Name = "button6"
		Me.button6.Size = New System.Drawing.Size(60, 22)
		Me.button6.TabIndex = 9
		Me.button6.Text = "Teilen"
		Me.button6.UseVisualStyleBackColor = true
		AddHandler Me.button6.Click, AddressOf Me.Button6Click
		'
		'button7
		'
		Me.button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button7.Location = New System.Drawing.Point(818, 71)
		Me.button7.Name = "button7"
		Me.button7.Size = New System.Drawing.Size(95, 21)
		Me.button7.TabIndex = 10
		Me.button7.Text = "Importieren"
		Me.button7.UseVisualStyleBackColor = true
		AddHandler Me.button7.Click, AddressOf Me.Button7Click
		'
		'ScriptEditor
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(925, 484)
		Me.Controls.Add(Me.button7)
		Me.Controls.Add(Me.button6)
		Me.Controls.Add(Me.label4)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.statusStrip1)
		Me.Controls.Add(Me.scriptTypeSelection)
		Me.Controls.Add(Me.button4)
		Me.Controls.Add(Me.button3)
		Me.Controls.Add(Me.textBox2)
		Me.Controls.Add(Me.richTextBox1)
		Me.Controls.Add(Me.textBox1)
		Me.Controls.Add(Me.button5)
		Me.Controls.Add(Me.button2)
		Me.Controls.Add(Me.button1)
		Me.Controls.Add(Me.comboBox1)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.Name = "ScriptEditor"
		Me.Text = "ScriptEditor"
		AddHandler Load, AddressOf Me.ScriptEditorLoad
		Me.statusStrip1.ResumeLayout(false)
		Me.statusStrip1.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private button7 As System.Windows.Forms.Button
	Private button6 As System.Windows.Forms.Button
	Private label4 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private toolStripStatusLabel7 As System.Windows.Forms.ToolStripStatusLabel
	Private toolStripStatusLabel6 As System.Windows.Forms.ToolStripStatusLabel
	Private toolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel
	Private toolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
	Private toolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
	Private toolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
	Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
	Private statusStrip1 As System.Windows.Forms.StatusStrip
	Private timer1 As System.Windows.Forms.Timer
	Private button5 As System.Windows.Forms.Button
	Private scriptTypeSelection As System.Windows.Forms.ComboBox
	Private button4 As System.Windows.Forms.Button
	Private button3 As System.Windows.Forms.Button
	Private textBox2 As System.Windows.Forms.TextBox
	Private richTextBox1 As System.Windows.Forms.RichTextBox
	Private textBox1 As System.Windows.Forms.TextBox
	Private button2 As System.Windows.Forms.Button
	Private button1 As System.Windows.Forms.Button
	Private comboBox1 As System.Windows.Forms.ComboBox
End Class
