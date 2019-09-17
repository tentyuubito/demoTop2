Public Class frmSelect_Packing

    Private _PackingType As ePackingType = ePackingType.None

    Public Enum ePackingType
        None = 0
        CL
        SL
    End Enum

    Public ReadOnly Property PackingType() As ePackingType
        Get
            Return Me._PackingType
        End Get
    End Property

    Private Sub btnCL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCL.Click
        Me._PackingType = ePackingType.CL
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSL.Click
        Me._PackingType = ePackingType.SL
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub


    Private Sub btnExcelDrop_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelDrop.Click
        Try
            Dim frm As New frmPackingBox_Topcharoen
            frm.ShowDialog()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub
End Class