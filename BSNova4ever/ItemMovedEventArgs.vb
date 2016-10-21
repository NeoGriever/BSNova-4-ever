Option Explicit On
Option Strict On
Option Infer Off

<System.Diagnostics.DebuggerStepThrough()> _
Public Class ItemMovedEventArgs
    Inherits System.EventArgs

    Private _CurrentIndex, _NewIndex As Integer

    Public Sub New(ByVal CurrentIndex As Integer, ByVal NewIndex As Integer)
        _CurrentIndex = If(CurrentIndex > -2I, CurrentIndex, -1I)
        _NewIndex = If(NewIndex > -2I, NewIndex, -1I)
    End Sub

#Region " Properties: CurrentIndex, NewIndex "

    Public ReadOnly Property CurrentIndex() As Integer
        Get
            Return _CurrentIndex
        End Get
    End Property

    Public ReadOnly Property NewIndex() As Integer
        Get
            Return _NewIndex
        End Get
    End Property

#End Region

End Class