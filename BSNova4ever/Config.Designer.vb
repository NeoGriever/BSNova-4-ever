'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 10.10.2016
' Zeit: 11:47
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class Config
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
		Me.button1 = New System.Windows.Forms.Button()
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.label2 = New System.Windows.Forms.Label()
		Me.textBox2 = New System.Windows.Forms.TextBox()
		Me.button2 = New System.Windows.Forms.Button()
		Me.textBox3 = New System.Windows.Forms.TextBox()
		Me.label3 = New System.Windows.Forms.Label()
		Me.button4 = New System.Windows.Forms.Button()
		Me.listView1 = New System.Windows.Forms.ListView()
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader()
		Me.label4 = New System.Windows.Forms.Label()
		Me.moveItemListView1 = New BSNova4ever.MoveItemListView()
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader()
		Me.label5 = New System.Windows.Forms.Label()
		Me.SuspendLayout
		'
		'button1
		'
		Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button1.Location = New System.Drawing.Point(652, 8)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(35, 22)
		Me.button1.TabIndex = 0
		Me.button1.Text = "..."
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'textBox1
		'
		Me.textBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox1.Location = New System.Drawing.Point(117, 9)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.ReadOnly = true
		Me.textBox1.Size = New System.Drawing.Size(529, 20)
		Me.textBox1.TabIndex = 1
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(11, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(100, 20)
		Me.label1.TabIndex = 2
		Me.label1.Text = "Download-Pfad:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'label2
		'
		Me.label2.Location = New System.Drawing.Point(11, 31)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(100, 20)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Filme-Pattern:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'textBox2
		'
		Me.textBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox2.Location = New System.Drawing.Point(117, 31)
		Me.textBox2.Name = "textBox2"
		Me.textBox2.Size = New System.Drawing.Size(529, 20)
		Me.textBox2.TabIndex = 1
		AddHandler Me.textBox2.TextChanged, AddressOf Me.TextBox2TextChanged
		'
		'button2
		'
		Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button2.Location = New System.Drawing.Point(652, 31)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(35, 43)
		Me.button2.TabIndex = 3
		Me.button2.Text = "?"
		Me.button2.UseVisualStyleBackColor = true
		AddHandler Me.button2.Click, AddressOf Me.Button2Click
		'
		'textBox3
		'
		Me.textBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox3.Location = New System.Drawing.Point(117, 54)
		Me.textBox3.Name = "textBox3"
		Me.textBox3.Size = New System.Drawing.Size(529, 20)
		Me.textBox3.TabIndex = 2
		AddHandler Me.textBox3.TextChanged, AddressOf Me.TextBox2TextChanged
		'
		'label3
		'
		Me.label3.Location = New System.Drawing.Point(11, 54)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(101, 20)
		Me.label3.TabIndex = 2
		Me.label3.Text = "Serien-Pattern:"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'button4
		'
		Me.button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button4.Location = New System.Drawing.Point(526, 372)
		Me.button4.Name = "button4"
		Me.button4.Size = New System.Drawing.Size(161, 23)
		Me.button4.TabIndex = 5
		Me.button4.Text = "Speichern"
		Me.button4.UseVisualStyleBackColor = true
		AddHandler Me.button4.Click, AddressOf Me.Button4Click
		'
		'listView1
		'
		Me.listView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.listView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1})
		Me.listView1.FullRowSelect = true
		Me.listView1.GridLines = true
		Me.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
		Me.listView1.Location = New System.Drawing.Point(117, 76)
		Me.listView1.Name = "listView1"
		Me.listView1.Size = New System.Drawing.Size(570, 104)
		Me.listView1.TabIndex = 4
		Me.listView1.UseCompatibleStateImageBehavior = false
		Me.listView1.View = System.Windows.Forms.View.Details
		'
		'columnHeader1
		'
		Me.columnHeader1.Width = 548
		'
		'label4
		'
		Me.label4.Location = New System.Drawing.Point(10, 76)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(101, 20)
		Me.label4.TabIndex = 2
		Me.label4.Text = "Pattern-Vorschau:"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'moveItemListView1
		'
		Me.moveItemListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.moveItemListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader2})
		Me.moveItemListView1.FullRowSelect = true
		Me.moveItemListView1.GridLines = true
		Me.moveItemListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
		Me.moveItemListView1.InsertionLineColor = System.Drawing.Color.Red
		Me.moveItemListView1.Location = New System.Drawing.Point(117, 186)
		Me.moveItemListView1.Name = "moveItemListView1"
		Me.moveItemListView1.Size = New System.Drawing.Size(570, 180)
		Me.moveItemListView1.TabIndex = 6
		Me.moveItemListView1.UseCompatibleStateImageBehavior = false
		Me.moveItemListView1.View = System.Windows.Forms.View.Details
		'
		'columnHeader2
		'
		Me.columnHeader2.Width = 548
		'
		'label5
		'
		Me.label5.Location = New System.Drawing.Point(12, 186)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(101, 20)
		Me.label5.TabIndex = 2
		Me.label5.Text = "Hoster-Reihenfolge:"
		Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Config
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(699, 404)
		Me.Controls.Add(Me.moveItemListView1)
		Me.Controls.Add(Me.listView1)
		Me.Controls.Add(Me.button4)
		Me.Controls.Add(Me.label5)
		Me.Controls.Add(Me.label4)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.textBox3)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.textBox2)
		Me.Controls.Add(Me.textBox1)
		Me.Controls.Add(Me.button2)
		Me.Controls.Add(Me.button1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "Config"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Einstellungen"
		AddHandler Load, AddressOf Me.ConfigLoad
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private label5 As System.Windows.Forms.Label
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private moveItemListView1 As BSNova4ever.MoveItemListView
	Private label4 As System.Windows.Forms.Label
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private listView1 As System.Windows.Forms.ListView
	Private button4 As System.Windows.Forms.Button
	Private label3 As System.Windows.Forms.Label
	Private textBox3 As System.Windows.Forms.TextBox
	Private button2 As System.Windows.Forms.Button
	Private textBox2 As System.Windows.Forms.TextBox
	Private label2 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private textBox1 As System.Windows.Forms.TextBox
	Private button1 As System.Windows.Forms.Button
End Class
