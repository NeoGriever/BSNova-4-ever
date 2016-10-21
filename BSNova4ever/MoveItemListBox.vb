Option Explicit On
Option Strict On
Option Infer Off
Imports System

Imports System.ComponentModel

<System.Diagnostics.DebuggerStepThrough(), ToolboxBitmap(GetType(ListBox))> _
Public Class MoveItemListBox
    Inherits System.Windows.Forms.ListBox

    'Based on:
    'mav.northwind http://www.codeproject.com/Members/mav-northwind
    'http://www.codeproject.com/KB/list/LVCustomReordering.aspx

#Region " Variables "

    Private Const WM_PAINT As Integer = &HF
    Private _LineBefore As Integer = -1I
    Private _LineAfter As Integer = -1I
    Private _itemDnD As Object = Nothing
    Private _itemIdx As Integer = -1I

    Private m_ItemMoveEnabled As Boolean
    Private m_InserLineClr As Color

    Public Event ItemMoved As EventHandler(Of ItemMovedEventArgs)
    Public Event ItemMoveEnabledChanged As EventHandler

    Private m_InserLineBr As SolidBrush
    Private m_InserLinePn As Pen

#End Region
#Region " Constructors "

    Public Sub New()
        'Reduce flicker
        Me.DoubleBuffered = True
        'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.SelectionMode = System.Windows.Forms.SelectionMode.One
        m_ItemMoveEnabled = True
        m_InserLineClr = Color.Red
        m_InserLineBr = New SolidBrush(m_InserLineClr)
        m_InserLinePn = New Pen(m_InserLineClr, 1.0!)
    End Sub

#End Region
#Region " Properties: ItemMoveEnabled, InsertionLineColor, MultiSelect "

    <Browsable(True), Category("Items"), DefaultValue(True)> _
    Public Property ItemMoveEnabled() As Boolean
        Get
            Return m_ItemMoveEnabled
        End Get
        Set(ByVal value As Boolean)
            If m_ItemMoveEnabled <> value Then
                m_ItemMoveEnabled = value
                Call OnItemMoveEnabledChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <Browsable(True), Category("Items"), DefaultValue("Red")> _
    Public Property InsertionLineColor() As Color
        Get
            Return m_InserLineClr
        End Get
        Set(ByVal value As Color)
            If Not m_InserLineClr.Equals(value) Then
                m_InserLineClr = value
                m_InserLineBr.Dispose()
                m_InserLinePn.Dispose()
                m_InserLineBr = New SolidBrush(m_InserLineClr)
                m_InserLinePn = New Pen(m_InserLineClr, 1.0!)
            End If
        End Set
    End Property

    <Browsable(False), EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), _
    DefaultValue(System.Windows.Forms.SelectionMode.One)> _
    Public Shadows ReadOnly Property SelectionMode() As System.Windows.Forms.SelectionMode
        Get
            Return MyBase.SelectionMode
        End Get
    End Property

#End Region
#Region " Overrides: WndProc, Dispose "
    <System.Diagnostics.DebuggerStepThrough()> _
    Protected Overloads Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)

        ' We have to take this way (instead of overriding OnPaint()) because the ListView is just a wrapper
        ' around the common control ListView and unfortunately does not call the OnPaint overrides.
        If m.Msg = WM_PAINT Then
            If _LineBefore >= 0I AndAlso _LineBefore < Items.Count Then
                Dim rc As Rectangle = Me.GetItemRectangle(_LineBefore)
                Call DrawInsertionLine(rc.Left, rc.Right, rc.Top)
            End If
            If _LineAfter >= 0I AndAlso _LineBefore < Items.Count Then
                Dim rc As Rectangle = Me.GetItemRectangle(_LineAfter)
                Call DrawInsertionLine(rc.Left, rc.Right, rc.Bottom)
            End If
        End If
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            'The object is being explicitly disposed so dispose its children.
            m_InserLineBr.Dispose()
            m_InserLinePn.Dispose()
        End If
        m_InserLineBr = Nothing
        m_InserLinePn = Nothing
        MyBase.Dispose(disposing)
    End Sub

#End Region
#Region " DrawInsertionLine "

    ''' <summary>
    ''' Draw a line with insertion marks at each end
    ''' </summary>
    ''' <param name="X1">Starting position (X) of the line</param>
    ''' <param name="X2">Ending position (X) of the line</param>
    ''' <param name="Y">Position (Y) of the line</param>
    Private Sub DrawInsertionLine(ByVal X1 As Integer, ByVal X2 As Integer, ByVal Y As Integer)
        Using g As Graphics = Me.CreateGraphics()
            g.DrawLine(m_InserLinePn, X1, Y, X2 - 1I, Y)

            Dim leftTriangle() As Point = {New Point(X1, Y - 4I), New Point(X1 + 7I, Y), New Point(X1, Y + 4I)}
            Dim rightTriangle() As Point = {New Point(X2, Y - 4I), New Point(X2 - 8I, Y), New Point(X2, Y + 4I)}
            g.FillPolygon(m_InserLineBr, leftTriangle)
            g.FillPolygon(m_InserLineBr, rightTriangle)
        End Using
    End Sub

#End Region
#Region " Mouse: Down, Move, Up "

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If Me.ItemMoveEnabled AndAlso Me.Items.Count > 1I Then
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                _itemIdx = Me.IndexFromPoint(New Point(e.X, e.Y))
                If _itemIdx > -1I Then _itemDnD = MyBase.Items(_itemIdx)
            End If
        End If
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        If _itemDnD IsNot Nothing Then
            ' Show the user that a drag operation is happening
            Me.Cursor = Cursors.Hand

            ' calculate the bottom of the last item in the LB so that you don't have to stop your drag at the last item
            Dim lastItemBottom As Integer = Math.Min(e.Y, MyBase.GetItemRectangle(MyBase.Items.Count - 1I).Bottom - 1I)
            Dim NewY As Integer = e.Y
            ' use 0 instead of e.X so that you don't have to keep inside the columns while dragging
            Select Case e.Y
                Case Is < 0I : NewY = 0I
                Case Is > lastItemBottom : NewY = lastItemBottom
            End Select
            Dim itemOver As Integer = MyBase.IndexFromPoint(New Point(0I, NewY))

            If itemOver > -1I Then
                Dim rc As Rectangle = MyBase.GetItemRectangle(itemOver)
                If e.Y < rc.Top + (rc.Height / 2I) Then
                    _LineBefore = itemOver
                    _LineAfter = -1I
                Else
                    _LineBefore = -1I
                    _LineAfter = itemOver
                End If
                Me.SelectedIndex = _itemIdx
                ' invalidate the LB so that the insertion line is shown
                Me.Invalidate()
            End If
        End If
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If _itemDnD IsNot Nothing Then
                Dim RaiseItemMoved As Boolean = False
                Dim OldIdx As Integer = _itemIdx
                Dim NewIdx As Integer = -1I
                Try
                    Dim lastItemBottom As Integer = Math.Min(e.Y, MyBase.GetItemRectangle(MyBase.Items.Count - 1I).Bottom - 1I)
                    Dim NewY As Integer = e.Y
                    Select Case e.Y
                        Case Is < 0I : NewY = 0I
                        Case Is > lastItemBottom : NewY = lastItemBottom
                    End Select

                    Dim itemOver As Integer = Me.IndexFromPoint(New Point(0I, NewY))
                    If itemOver > -1I Then
                        If _itemDnD IsNot Me.Items(itemOver) Then
                            MyBase.Items.Remove(_itemDnD)
                            MyBase.Items.Insert(itemOver, _itemDnD)
                            NewIdx = itemOver
                            RaiseItemMoved = True
                        End If
                        Me.SelectedIndex = itemOver
                    End If
                Finally
                    _itemDnD = Nothing
                    _itemIdx = -1I
                    Me.Cursor = Cursors.Default
                    _LineAfter = -1I
                    _LineBefore = -1I
                    Me.Invalidate()
                End Try
                If RaiseItemMoved Then Call OnItemMoved(New ItemMovedEventArgs(OldIdx, NewIdx))
            End If
        End If
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        _LineAfter = -1I
        _LineBefore = -1I
        _itemDnD = Nothing
        _itemIdx = -1I
        Me.Cursor = Cursors.Default
        Me.Invalidate()
    End Sub

#End Region
#Region " EventHandlers: OnItemMoved, OnItemMoveEnabledChanged "

    Protected Sub OnItemMoved(ByVal e As ItemMovedEventArgs)
        RaiseEvent ItemMoved(Me, e)
    End Sub

    Protected Sub OnItemMoveEnabledChanged(ByVal e As System.EventArgs)
        RaiseEvent ItemMoveEnabledChanged(Me, e)
    End Sub

#End Region

End Class