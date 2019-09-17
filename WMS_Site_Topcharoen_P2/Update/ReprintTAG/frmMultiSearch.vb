Public Class frmMultiSearch

    Private _ArrDocument_No As String = ""

    Public ReadOnly Property ArrDocument_No() As String
        Get
            Return _ArrDocument_No
        End Get
    End Property


    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            _ArrDocument_No = ""
            Dim strLine As String = ""
            Dim strArrLine() As String = Me.TextBox1.Text.Split(vbNewLine)
            Dim iStart As Integer = 0
            For Each strLine In strArrLine
                If strLine.ToString.Trim <> "" Then
                    If iStart = 0 Then
                        _ArrDocument_No &= "'" & strLine.Trim & "'"
                    Else
                        _ArrDocument_No &= ",'" & strLine.Trim & "'"
                    End If
                    iStart += 1
                End If

            Next
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmMultiSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.TextBox1.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class