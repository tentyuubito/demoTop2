Imports System.Windows.Forms
Imports WMS_STD_Master.W_Language
Imports System.Collections.Generic

Public Class Default_Key : Implements IDisposable

    Private frm As Form
    Public Sub escForExit(ByVal frm As Form)
        Try
            Me.frm = frm
            Me.addHandle()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Public Sub Ctrl_A_SelectALL(ByVal frm As Form)
        Try
            If frm Is Nothing Then Exit Sub
            For Each ctrl As Control In frm.Controls
                If TypeOf ctrl Is GroupBox Then
                    For Each group As Control In ctrl.Controls
                        If TypeOf group Is TextBox Then
                            AddHandler group.KeyDown, AddressOf Me.SelectAll
                        End If
                    Next
                ElseIf TypeOf ctrl Is TextBox Then
                    AddHandler ctrl.KeyDown, AddressOf Me.SelectAll
                End If
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private keyCheck As New List(Of keyAndCheck)
    Public Sub keyAndcheck(ByVal txt As TextBox, ByVal chk As CheckBox)
        Try
            If txt IsNot Nothing AndAlso chk IsNot Nothing Then
                Dim key As New keyAndCheck
                key.txt = txt
                key.chk = chk
                keyCheck.Add(key)
                AddHandler txt.TextChanged, AddressOf txtKeyChanged
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtKeyChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            For Each key As keyAndCheck In keyCheck
                If key.txt.Equals(CType(sender, TextBox)) Then
                    key.chk.Checked = CType(sender, TextBox).Text.Trim.Length > 0
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectAll(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.Control And e.KeyCode = Keys.A Then
                CType(sender, TextBox).SelectAll()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub addHandle()
        Try
            If Me.frm Is Nothing Then Exit Sub
            AddHandler Me.frm.KeyDown, AddressOf Esc
            For Each ctrl As Control In Me.frm.Controls
                If TypeOf ctrl Is GroupBox Then
                    For Each group As Control In ctrl.Controls
                        AddHandler group.KeyDown, AddressOf Esc
                    Next
                End If
                AddHandler ctrl.KeyDown, AddressOf Esc
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Esc(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Escape Then
                frm.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub NumericOnly(ByVal ParamArray txt() As TextBox)
        Try
            For Each t As TextBox In txt
                AddHandler t.KeyPress, AddressOf Numeric
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub Numeric(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim txt = DirectCast(sender, TextBox)
        If (e.KeyChar = ChrW(Keys.Back)) Then
        ElseIf (Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "."))) Then
            e.Handled = True
        End If
    End Sub

    Private disposedValue As Boolean = False
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

Public Structure keyAndCheck
    Private _txt As TextBox
    Public Property txt() As TextBox
        Get
            Return Me._txt
        End Get
        Set(ByVal value As TextBox)
            Me._txt = value
        End Set
    End Property
    Private _chk As CheckBox
    Public Property chk() As CheckBox
        Get
            Return Me._chk
        End Get
        Set(ByVal value As CheckBox)
            Me._chk = value
        End Set
    End Property
End Structure
