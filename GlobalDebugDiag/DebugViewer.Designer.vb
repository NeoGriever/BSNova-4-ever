'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 07.10.2016
' Zeit: 15:07
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class DebugViewer
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
		Me.listView1 = New System.Windows.Forms.ListView()
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader()
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader()
		Me.columnHeader3 = New System.Windows.Forms.ColumnHeader()
		Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
		Me.logdateiÜbermittelnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.menuStrip1.SuspendLayout
		Me.SuspendLayout
		'
		'listView1
		'
		Me.listView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2, Me.columnHeader3})
		Me.listView1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.listView1.FullRowSelect = true
		Me.listView1.GridLines = true
		Me.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
		Me.listView1.HideSelection = false
		Me.listView1.Location = New System.Drawing.Point(0, 24)
		Me.listView1.MultiSelect = false
		Me.listView1.Name = "listView1"
		Me.listView1.Size = New System.Drawing.Size(764, 277)
		Me.listView1.TabIndex = 0
		Me.listView1.UseCompatibleStateImageBehavior = false
		Me.listView1.View = System.Windows.Forms.View.Details
		AddHandler Me.listView1.ItemActivate, AddressOf Me.ListView1ItemActivate
		AddHandler Me.listView1.SizeChanged, AddressOf Me.ListView1SizeChanged
		'
		'columnHeader1
		'
		Me.columnHeader1.Width = 109
		'
		'columnHeader2
		'
		Me.columnHeader2.Width = 523
		'
		'columnHeader3
		'
		Me.columnHeader3.Width = 111
		'
		'menuStrip1
		'
		Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.logdateiÜbermittelnToolStripMenuItem})
		Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.menuStrip1.Name = "menuStrip1"
		Me.menuStrip1.Size = New System.Drawing.Size(764, 24)
		Me.menuStrip1.TabIndex = 1
		Me.menuStrip1.Text = "menuStrip1"
		'
		'logdateiÜbermittelnToolStripMenuItem
		'
		Me.logdateiÜbermittelnToolStripMenuItem.Name = "logdateiÜbermittelnToolStripMenuItem"
		Me.logdateiÜbermittelnToolStripMenuItem.Size = New System.Drawing.Size(142, 20)
		Me.logdateiÜbermittelnToolStripMenuItem.Text = "Logdatei übermitteln ..."
		AddHandler Me.logdateiÜbermittelnToolStripMenuItem.Click, AddressOf Me.LogdateiÜbermittelnToolStripMenuItemClick
		'
		'DebugViewer
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(764, 301)
		Me.Controls.Add(Me.listView1)
		Me.Controls.Add(Me.menuStrip1)
		Me.MainMenuStrip = Me.menuStrip1
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "DebugViewer"
		Me.ShowIcon = false
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Log"
		AddHandler Load, AddressOf Me.DebugViewerLoad
		Me.menuStrip1.ResumeLayout(false)
		Me.menuStrip1.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private logdateiÜbermittelnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private menuStrip1 As System.Windows.Forms.MenuStrip
	Private columnHeader3 As System.Windows.Forms.ColumnHeader
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private listView1 As System.Windows.Forms.ListView
End Class
