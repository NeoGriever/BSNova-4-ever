'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 07.10.2016
' Zeit: 18:47
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class DebugViewerDetails
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
		Me.richTextBox1 = New System.Windows.Forms.RichTextBox()
		Me.label2 = New System.Windows.Forms.Label()
		Me.SuspendLayout
		'
		'label1
		'
		Me.label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label1.Location = New System.Drawing.Point(12, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(158, 17)
		Me.label1.TabIndex = 0
		Me.label1.Text = "DATUM"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'richTextBox1
		'
		Me.richTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.richTextBox1.DetectUrls = false
		Me.richTextBox1.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.richTextBox1.HideSelection = false
		Me.richTextBox1.Location = New System.Drawing.Point(12, 29)
		Me.richTextBox1.Name = "richTextBox1"
		Me.richTextBox1.ReadOnly = true
		Me.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
		Me.richTextBox1.Size = New System.Drawing.Size(284, 175)
		Me.richTextBox1.TabIndex = 1
		Me.richTextBox1.Text = ""
		'
		'label2
		'
		Me.label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label2.Location = New System.Drawing.Point(176, 9)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(120, 17)
		Me.label2.TabIndex = 2
		Me.label2.Text = "TYPE"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'DebugViewerDetails
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(308, 216)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.richTextBox1)
		Me.Controls.Add(Me.label1)
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "DebugViewerDetails"
		Me.ShowIcon = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Details"
		Me.ResumeLayout(false)
	End Sub
	Private label2 As System.Windows.Forms.Label
	Private richTextBox1 As System.Windows.Forms.RichTextBox
	Private label1 As System.Windows.Forms.Label
End Class
