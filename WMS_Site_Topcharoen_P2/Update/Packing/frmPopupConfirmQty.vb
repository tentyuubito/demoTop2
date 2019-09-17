Imports WMS_STD_Master.W_Language

Public Class frmPopupConfirmQty

    Private _ConfirmQty As Long

    Public Sub New(ByVal ConfirmQty As Long, ByVal SalesOrderNo As String, ByVal SkuDescription As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.txtConfirmQty.Text = ConfirmQty
        Me._ConfirmQty = ConfirmQty

        'Me.lblSO.Text &= SalesOrderNo
        Me.lblSku.Text &= SkuDescription
    End Sub

    Private Sub ConfirmQty()
        Try
            Dim KeyQty As String = Me.txtQty.Text.Trim

            If String.IsNullOrEmpty(KeyQty) Then
                Throw New Exception("กรุณาระบุจำนวน")
            End If

            If IsNumeric(KeyQty) Then
                Throw New Exception("กรุณาระบุตัวเลขเท่านั้น")
            End If

            If Long.Parse(KeyQty) <> Me._ConfirmQty Then
                Throw New Exception("จำนวนที่ยืนยันไม่ถูกต้อง")
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        If e.KeyChar <> ChrW(13) Then
            Exit Sub
        End If

        Try
            ConfirmQty()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
            Me.txtConfirmQty.Clear()
            Me.txtConfirmQty.Focus()
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            ConfirmQty()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
            Me.txtConfirmQty.Clear()
            Me.txtConfirmQty.Focus()
        End Try
    End Sub

    Private Sub btnFrmClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class

