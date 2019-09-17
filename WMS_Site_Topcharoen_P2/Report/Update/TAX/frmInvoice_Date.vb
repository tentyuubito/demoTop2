Public Class frmInvoice_Date
    Public isCancel As Boolean = False



    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        isCancel = True
        Me.Close()
    End Sub

    Private Sub btn_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_select.Click
        Me.Close()
    End Sub

    Private Sub frmInvoice_Date_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpInvoice_Date.Value = Now.AddDays(1)
    End Sub
End Class