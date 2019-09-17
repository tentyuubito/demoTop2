Public Class frmCopy_SO
    Public isCancel As Boolean = False
    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        isCancel = True
        Me.Close()
    End Sub

    Private Sub btn_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_select.Click
        Me.Close()
    End Sub

    Private Sub frmCopy_SO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class