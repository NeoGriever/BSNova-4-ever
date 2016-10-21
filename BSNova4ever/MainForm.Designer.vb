'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 18.09.2016
' Zeit: 17:00
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class MainForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.treeView1 = New BSNova4ever.treeViewDoubleBuffered()
		Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.gesamteStaffelHerunterladenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.episodeHerunterladenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.hosterLinkKopierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.panel1 = New System.Windows.Forms.Panel()
		Me.toolStrip2 = New System.Windows.Forms.ToolStrip()
		Me.filterBox = New System.Windows.Forms.ToolStripTextBox()
		Me.toolStripButton4 = New System.Windows.Forms.ToolStripButton()
		Me.listView1 = New System.Windows.Forms.ListView()
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader()
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader()
		Me.contextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.startToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.stopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.entfernenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
		Me.neustartenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
		Me.scriptEditorÖffnenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.einstellungenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.neueVersionHerunterladenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.timer2 = New System.Windows.Forms.Timer(Me.components)
		Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.contextMenuStrip1.SuspendLayout
		CType(Me.splitContainer1,System.ComponentModel.ISupportInitialize).BeginInit
		Me.splitContainer1.Panel1.SuspendLayout
		Me.splitContainer1.Panel2.SuspendLayout
		Me.splitContainer1.SuspendLayout
		Me.panel1.SuspendLayout
		Me.toolStrip2.SuspendLayout
		Me.contextMenuStrip2.SuspendLayout
		Me.menuStrip1.SuspendLayout
		Me.statusStrip1.SuspendLayout
		Me.SuspendLayout
		'
		'treeView1
		'
		Me.treeView1.ContextMenuStrip = Me.contextMenuStrip1
		Me.treeView1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.treeView1.Location = New System.Drawing.Point(0, 0)
		Me.treeView1.Name = "treeView1"
		Me.treeView1.Size = New System.Drawing.Size(520, 254)
		Me.treeView1.TabIndex = 0
		AddHandler Me.treeView1.AfterSelect, AddressOf Me.TreeView1AfterSelect
		AddHandler Me.treeView1.DoubleClick, AddressOf Me.TreeView1DoubleClick
		'
		'contextMenuStrip1
		'
		Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.gesamteStaffelHerunterladenToolStripMenuItem, Me.episodeHerunterladenToolStripMenuItem, Me.hosterLinkKopierenToolStripMenuItem})
		Me.contextMenuStrip1.Name = "contextMenuStrip1"
		Me.contextMenuStrip1.Size = New System.Drawing.Size(234, 70)
		AddHandler Me.contextMenuStrip1.Opening, AddressOf Me.ContextMenuStrip1Opening
		'
		'gesamteStaffelHerunterladenToolStripMenuItem
		'
		Me.gesamteStaffelHerunterladenToolStripMenuItem.Name = "gesamteStaffelHerunterladenToolStripMenuItem"
		Me.gesamteStaffelHerunterladenToolStripMenuItem.Size = New System.Drawing.Size(233, 22)
		Me.gesamteStaffelHerunterladenToolStripMenuItem.Text = "Gesamte Staffel herunterladen"
		Me.gesamteStaffelHerunterladenToolStripMenuItem.Visible = false
		'
		'episodeHerunterladenToolStripMenuItem
		'
		Me.episodeHerunterladenToolStripMenuItem.Name = "episodeHerunterladenToolStripMenuItem"
		Me.episodeHerunterladenToolStripMenuItem.Size = New System.Drawing.Size(233, 22)
		Me.episodeHerunterladenToolStripMenuItem.Text = "Episode herunterladen"
		Me.episodeHerunterladenToolStripMenuItem.Visible = false
		'
		'hosterLinkKopierenToolStripMenuItem
		'
		Me.hosterLinkKopierenToolStripMenuItem.Name = "hosterLinkKopierenToolStripMenuItem"
		Me.hosterLinkKopierenToolStripMenuItem.Size = New System.Drawing.Size(233, 22)
		Me.hosterLinkKopierenToolStripMenuItem.Text = "Hoster-Link kopieren"
		Me.hosterLinkKopierenToolStripMenuItem.Visible = false
		AddHandler Me.hosterLinkKopierenToolStripMenuItem.Click, AddressOf Me.HosterLinkKopierenToolStripMenuItemClick
		'
		'splitContainer1
		'
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.Location = New System.Drawing.Point(0, 24)
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.panel1)
		Me.splitContainer1.Panel1.Controls.Add(Me.toolStrip2)
		Me.splitContainer1.Panel1MinSize = 150
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.listView1)
		Me.splitContainer1.Panel2.Controls.Add(Me.toolStrip1)
		Me.splitContainer1.Panel2MinSize = 150
		Me.splitContainer1.Size = New System.Drawing.Size(880, 279)
		Me.splitContainer1.SplitterDistance = 520
		Me.splitContainer1.TabIndex = 1
		AddHandler Me.splitContainer1.SplitterMoved, AddressOf Me.SplitContainer1SplitterMoved
		'
		'panel1
		'
		Me.panel1.Controls.Add(Me.treeView1)
		Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel1.Location = New System.Drawing.Point(0, 25)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(520, 254)
		Me.panel1.TabIndex = 2
		'
		'toolStrip2
		'
		Me.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.toolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.filterBox, Me.toolStripButton4})
		Me.toolStrip2.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip2.Name = "toolStrip2"
		Me.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
		Me.toolStrip2.Size = New System.Drawing.Size(520, 25)
		Me.toolStrip2.TabIndex = 1
		Me.toolStrip2.Text = "toolStrip2"
		'
		'filterBox
		'
		Me.filterBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.filterBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
		Me.filterBox.Name = "filterBox"
		Me.filterBox.Size = New System.Drawing.Size(140, 25)
		Me.filterBox.ToolTipText = "Filter"
		AddHandler Me.filterBox.TextChanged, AddressOf Me.FilterBoxTextChanged
		'
		'toolStripButton4
		'
		Me.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.toolStripButton4.Image = CType(resources.GetObject("toolStripButton4.Image"),System.Drawing.Image)
		Me.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.toolStripButton4.Name = "toolStripButton4"
		Me.toolStripButton4.Size = New System.Drawing.Size(23, 22)
		Me.toolStripButton4.Text = "Filter leeren"
		AddHandler Me.toolStripButton4.Click, AddressOf Me.ToolStripButton4Click
		'
		'listView1
		'
		Me.listView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2})
		Me.listView1.ContextMenuStrip = Me.contextMenuStrip2
		Me.listView1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.listView1.FullRowSelect = true
		Me.listView1.GridLines = true
		Me.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
		Me.listView1.HideSelection = false
		Me.listView1.Location = New System.Drawing.Point(0, 25)
		Me.listView1.Name = "listView1"
		Me.listView1.Size = New System.Drawing.Size(356, 254)
		Me.listView1.TabIndex = 0
		Me.listView1.UseCompatibleStateImageBehavior = false
		Me.listView1.View = System.Windows.Forms.View.Details
		AddHandler Me.listView1.ItemActivate, AddressOf Me.ListView1ItemActivate
		AddHandler Me.listView1.SelectedIndexChanged, AddressOf Me.ListView1SelectedIndexChanged
		AddHandler Me.listView1.SizeChanged, AddressOf Me.ListView1SizeChanged
		'
		'columnHeader1
		'
		Me.columnHeader1.Width = 208
		'
		'columnHeader2
		'
		Me.columnHeader2.Width = 124
		'
		'contextMenuStrip2
		'
		Me.contextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.startToolStripMenuItem, Me.stopToolStripMenuItem, Me.entfernenToolStripMenuItem, Me.toolStripMenuItem1, Me.neustartenToolStripMenuItem})
		Me.contextMenuStrip2.Name = "contextMenuStrip2"
		Me.contextMenuStrip2.Size = New System.Drawing.Size(133, 98)
		AddHandler Me.contextMenuStrip2.Opening, AddressOf Me.ContextMenuStrip2Opening
		'
		'startToolStripMenuItem
		'
		Me.startToolStripMenuItem.Name = "startToolStripMenuItem"
		Me.startToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.startToolStripMenuItem.Text = "Start"
		AddHandler Me.startToolStripMenuItem.Click, AddressOf Me.StartToolStripMenuItemClick
		'
		'stopToolStripMenuItem
		'
		Me.stopToolStripMenuItem.Name = "stopToolStripMenuItem"
		Me.stopToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.stopToolStripMenuItem.Text = "Stop"
		AddHandler Me.stopToolStripMenuItem.Click, AddressOf Me.StopToolStripMenuItemClick
		'
		'entfernenToolStripMenuItem
		'
		Me.entfernenToolStripMenuItem.Name = "entfernenToolStripMenuItem"
		Me.entfernenToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.entfernenToolStripMenuItem.Text = "Entfernen"
		AddHandler Me.entfernenToolStripMenuItem.Click, AddressOf Me.EntfernenToolStripMenuItemClick
		'
		'toolStripMenuItem1
		'
		Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
		Me.toolStripMenuItem1.Size = New System.Drawing.Size(129, 6)
		'
		'neustartenToolStripMenuItem
		'
		Me.neustartenToolStripMenuItem.Name = "neustartenToolStripMenuItem"
		Me.neustartenToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.neustartenToolStripMenuItem.Text = "Neustarten"
		AddHandler Me.neustartenToolStripMenuItem.Click, AddressOf Me.NeustartenToolStripMenuItemClick
		'
		'toolStrip1
		'
		Me.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
		Me.toolStrip1.Size = New System.Drawing.Size(356, 25)
		Me.toolStrip1.TabIndex = 1
		Me.toolStrip1.Text = "toolStrip1"
		'
		'timer1
		'
		Me.timer1.Interval = 450
		AddHandler Me.timer1.Tick, AddressOf Me.Timer1Tick
		'
		'menuStrip1
		'
		Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.scriptEditorÖffnenToolStripMenuItem, Me.einstellungenToolStripMenuItem, Me.neueVersionHerunterladenToolStripMenuItem})
		Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.menuStrip1.Name = "menuStrip1"
		Me.menuStrip1.Size = New System.Drawing.Size(880, 24)
		Me.menuStrip1.TabIndex = 2
		Me.menuStrip1.Text = "menuStrip1"
		'
		'scriptEditorÖffnenToolStripMenuItem
		'
		Me.scriptEditorÖffnenToolStripMenuItem.Name = "scriptEditorÖffnenToolStripMenuItem"
		Me.scriptEditorÖffnenToolStripMenuItem.Size = New System.Drawing.Size(123, 20)
		Me.scriptEditorÖffnenToolStripMenuItem.Text = "Script-Editor öffnen"
		AddHandler Me.scriptEditorÖffnenToolStripMenuItem.Click, AddressOf Me.ScriptEditorÖffnenToolStripMenuItemClick
		'
		'einstellungenToolStripMenuItem
		'
		Me.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem"
		Me.einstellungenToolStripMenuItem.Size = New System.Drawing.Size(90, 20)
		Me.einstellungenToolStripMenuItem.Text = "Einstellungen"
		AddHandler Me.einstellungenToolStripMenuItem.Click, AddressOf Me.EinstellungenToolStripMenuItemClick
		'
		'neueVersionHerunterladenToolStripMenuItem
		'
		Me.neueVersionHerunterladenToolStripMenuItem.BackColor = System.Drawing.Color.PowderBlue
		Me.neueVersionHerunterladenToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.neueVersionHerunterladenToolStripMenuItem.Image = CType(resources.GetObject("neueVersionHerunterladenToolStripMenuItem.Image"),System.Drawing.Image)
		Me.neueVersionHerunterladenToolStripMenuItem.Name = "neueVersionHerunterladenToolStripMenuItem"
		Me.neueVersionHerunterladenToolStripMenuItem.Size = New System.Drawing.Size(205, 20)
		Me.neueVersionHerunterladenToolStripMenuItem.Text = "Neue Version herunterladen ..."
		Me.neueVersionHerunterladenToolStripMenuItem.Visible = false
		AddHandler Me.neueVersionHerunterladenToolStripMenuItem.Click, AddressOf Me.NeueVersionHerunterladenToolStripMenuItemClick
		'
		'timer2
		'
		Me.timer2.Interval = 450
		AddHandler Me.timer2.Tick, AddressOf Me.Timer2Tick
		'
		'statusStrip1
		'
		Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabel1})
		Me.statusStrip1.Location = New System.Drawing.Point(0, 303)
		Me.statusStrip1.Name = "statusStrip1"
		Me.statusStrip1.Size = New System.Drawing.Size(880, 22)
		Me.statusStrip1.TabIndex = 3
		Me.statusStrip1.Text = "statusStrip1"
		'
		'toolStripStatusLabel1
		'
		Me.toolStripStatusLabel1.AutoSize = false
		Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
		Me.toolStripStatusLabel1.Size = New System.Drawing.Size(865, 17)
		Me.toolStripStatusLabel1.Spring = true
		Me.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		AddHandler Me.toolStripStatusLabel1.Click, AddressOf Me.ToolStripStatusLabel1Click
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(880, 325)
		Me.Controls.Add(Me.splitContainer1)
		Me.Controls.Add(Me.menuStrip1)
		Me.Controls.Add(Me.statusStrip1)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "BSNova4ever"
		AddHandler Load, AddressOf Me.MainFormLoad
		Me.contextMenuStrip1.ResumeLayout(false)
		Me.splitContainer1.Panel1.ResumeLayout(false)
		Me.splitContainer1.Panel1.PerformLayout
		Me.splitContainer1.Panel2.ResumeLayout(false)
		Me.splitContainer1.Panel2.PerformLayout
		CType(Me.splitContainer1,System.ComponentModel.ISupportInitialize).EndInit
		Me.splitContainer1.ResumeLayout(false)
		Me.panel1.ResumeLayout(false)
		Me.toolStrip2.ResumeLayout(false)
		Me.toolStrip2.PerformLayout
		Me.contextMenuStrip2.ResumeLayout(false)
		Me.menuStrip1.ResumeLayout(false)
		Me.menuStrip1.PerformLayout
		Me.statusStrip1.ResumeLayout(false)
		Me.statusStrip1.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private neueVersionHerunterladenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private einstellungenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private neustartenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
	Private entfernenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private stopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private startToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
	Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
	Private statusStrip1 As System.Windows.Forms.StatusStrip
	Private toolStripButton4 As System.Windows.Forms.ToolStripButton
	Private timer2 As System.Windows.Forms.Timer
	Private panel1 As System.Windows.Forms.Panel
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private filterBox As System.Windows.Forms.ToolStripTextBox
	Private toolStrip2 As System.Windows.Forms.ToolStrip
	Private scriptEditorÖffnenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private menuStrip1 As System.Windows.Forms.MenuStrip
	Private hosterLinkKopierenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private episodeHerunterladenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private gesamteStaffelHerunterladenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
	Private timer1 As System.Windows.Forms.Timer
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private listView1 As System.Windows.Forms.ListView
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private treeView1 As BSNova4ever.treeViewDoubleBuffered
End Class
