'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 09.10.2016
' Zeit: 13:29
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Partial Class ListViewDoubleBuffered
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the control.
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
		'
		'ListViewDoubleBuffered
		'
		Me.DoubleBuffered = True
		Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,True)
		Me.Name = "ListViewDoubleBuffered"
	End Sub
End Class
