Public Class frmExcelSelect_Import
    Private Sub frmExcelSelect_Import_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSL.Click
        Try
            Dim frm As New WMS_Site_TopCharoen.frmImport_SO_Top
            frm.Text = btnSL.Text
            frm.ShowDialog()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnExcelDrop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelDrop.Click
        Try
            Dim frm As New frmExcelDroppiont(frmExcelDroppiont.Type_Import.CONFIRM_Y)
            frm.Text = btnExcelDrop.Text
            frm.ShowDialog()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim frm As New frmExcelDroppiont(frmExcelDroppiont.Type_Import.CONFIRM_Z)
            frm.Text = Button1.Text
            frm.ShowDialog()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub
End Class