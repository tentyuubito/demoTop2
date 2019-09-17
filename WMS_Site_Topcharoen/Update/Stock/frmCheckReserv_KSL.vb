Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language

Public Class frmCheckReserv_KSL

    Dim xObjDB As New DBType_SQLServer
    Dim xObjDT As New DataTable
    Dim xObjSQL As String = ""


    Private _Sku_Index As String
    Public Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
        Set(ByVal value As String)
            _Sku_Index = value
        End Set
    End Property


    Private Sub frmCheckReserv_KSL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            xObjSQL = String.Format(" EXEC [dbo].[sp_KSL_Check_Reserve] '{0}'", Me._Sku_Index)
            xObjDT = xObjDB.DBExeQuery(xObjSQL)
            Me.grdData.AutoGenerateColumns = False
            Me.grdData.DataSource = xObjDT
            If xObjDT.Rows.Count > 0 Then
                Me.txtReserveQtySystem.Text = FormatNumber(xObjDT.Compute("SUM(Total_Qty)", ""), 4)
            End If

            'Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try
            If Me.grdData.RowCount = 0 Then Exit Sub
            If Me.grdData.Rows(Me.grdData.CurrentRow.Index).Cells("Document_No").Value.ToString.Substring(0, 3) = "TMP" Then
                W_MSG_Error("เอกสารยังไม่สมบูรณ์ไม่สามารถดูได้ Temp ของระบบ")
                Exit Sub
            End If

            Select Case Me.grdData.Rows(Me.grdData.CurrentRow.Index).Cells("Process_Id").Value.ToString
                Case "2"
                    Dim frm As New frmWithdrawAsset_V4(frmWithdrawAsset_V4.enuOperation_Type.UPDATE)
                    frm.Withdraw_Index = Me.grdData.Rows(Me.grdData.CurrentRow.Index).Cells("Document_Index").Value.ToString
                    frm.WithdrawStatus = True
                    frm.ShowDialog()
                Case "5"
                    Dim frm As New WMS_STD_TMM_TransferStatus.frmAssetTransfer_V2(WMS_STD_TMM_TransferStatus.frmAssetTransfer_V2.enuOperation_Type.EDIT)
                    frm.TransferStatus_Index = Me.grdData.Rows(Me.grdData.CurrentRow.Index).Cells("Document_Index").Value.ToString
                    frm.DocumentStatusValue = -1
                    frm.ShowDialog()
                Case Else
                    W_MSG_Error("ไม่สามารถดูเอกสารได้ ระบบยังไม่ Support")
                    Exit Sub
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
       
            Dim frm As New frmCheckReserveQty
            frm.SKU_Index = Me._Sku_Index
            frm.Show()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class