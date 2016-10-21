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
		Me.listView1 = New ListViewDoubleBuffered()
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader()
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader()
		Me.columnHeader3 = New System.Windows.Forms.ColumnHeader()
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
		Me.listView1.Location = New System.Drawing.Point(0, 0)
		Me.listView1.MultiSelect = false
		Me.listView1.Name = "listView1"
		Me.listView1.Size = New System.Drawing.Size(764, 301)
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
		'DebugViewer
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(764, 301)
		Me.Controls.Add(Me.listView1)
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "DebugViewer"
		Me.ShowIcon = false
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Log"
		AddHandler Load, AddressOf Me.DebugViewerLoad
		Me.ResumeLayout(false)
	End Sub
	Private columnHeader3 As System.Windows.Forms.ColumnHeader
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private listView1 As ListViewDoubleBuffered
End Class
