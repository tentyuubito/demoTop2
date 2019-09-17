Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports WMS_STD_OUTB_WithDraw_Datalayer

Public Class frmCheckWithdraw

#Region " VARIABLE & PROPERTY "

    Dim Withdraw_Index As String = ""
    Dim Customer_Shipping_Location_Index As String = ""
    Dim Withdraw_No As String = ""
    Dim Customer_Shipping_Location_Id As String = ""
    Dim SumQty As Decimal

    Dim dt_Data As New DataTable
    Dim _Serial_Data As New DataTable

#End Region

#Region " SUB & FUNCTION "

    Private Sub Init_property()
        Try
            Me.txtWithdraw_No.Clear()
            Me.txtCustomerShipping_Location.Clear()
            Me.txtWithdraw_No.Text = ""
            Me.txtCustomerShipping_Location.Text = ""
            Me.lblSumQty.Text = "ไม่มีรายการ"

            Me.grdData.AutoGenerateColumns = False

       

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ClearData()
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub LoadData(ByVal Withdraw_No As String, ByVal CustomerShipping_Location As String)
        Try
            Dim obj_WithdrawTrans As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.SEARCH)
            dt_Data = obj_WithdrawTrans.GetWithdrawbyCustomer(Withdraw_No, CustomerShipping_Location)

            If dt_Data.Rows.Count <= 0 Then
                Throw New Exception("ไม่พบข้อมูล")
            End If
            If dt_Data.Rows.Count > 0 Then
                grdData.DataSource = dt_Data

                SumQty = dt_Data.Compute("Sum(Qty)", "")
                Dim clsPacking As New Tb_Packing_TopCharoen
                lblSumQty.Text = clsPacking.checkBarcodeB1ByWithdraw(dt_Data.Rows(0).Item("withdraw_index")).ToString & "/" & CInt(SumQty).ToString 'Format(SumQty, "0.00")

                btnSave.Enabled = False
                If lblSumQty.Text.Split("/")(0) = lblSumQty.Text.Split("/")(1) Then

                    btnSave.Enabled = True
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

    Private Sub frmCheckWithdraw_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Init_property()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


    Private Sub txtWithdraw_No_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWithdraw_No.KeyPress
        Try
            If e.KeyChar <> ChrW(13) Then
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtWithdraw_No.Text) Then
                W_MSG_Information("กรุณากรอกเลขใบเบิกสินค้า")
                Me.txtWithdraw_No.Focus()
                Exit Sub
            End If

            If Not String.IsNullOrEmpty(txtCustomerShipping_Location.Text) Then
                LoadData(txtWithdraw_No.Text, txtCustomerShipping_Location.Text)
            End If

            Me.txtCustomerShipping_Location.Focus()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomerShipping_Location_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerShipping_Location.KeyPress
        Try
            If e.KeyChar <> ChrW(13) Then
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtCustomerShipping_Location.Text) Then
                W_MSG_Information("กรุณากรอกเลขใบเบิกสินค้า")
                Me.txtCustomerShipping_Location.Focus()
                Exit Sub
            End If

            If Not String.IsNullOrEmpty(txtWithdraw_No.Text) Then
                LoadData(txtWithdraw_No.Text, txtCustomerShipping_Location.Text)
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Me._Serial_Data = New DataTable
            Me.SumQty = 0
            txtWithdraw_No.Text = ""
            txtCustomerShipping_Location.Text = ""
            txtWithdraw_No.Focus()
            grdData.DataSource = Nothing
            lblSumQty.Text = ""
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try
            Dim frm As New frmWithdraw_ScanSerial
            frm.Total_Qty = Me.SumQty
            frm.TempdtWithdraw = grdData.DataSource

            If Me._Serial_Data.Rows.Count > 0 Then
                frm.dt_Serial = Me._Serial_Data
            End If
            frm.ShowDialog()

            If frm.checkSave = True Then
                Me._Serial_Data = frm.dt_Serial
                Dim clsPacking As New Tb_Packing_TopCharoen
                lblSumQty.Text = CInt(SumQty).ToString & "/" & CInt(SumQty).ToString 'Format(SumQty, "0.00")

                Me.btnSave.Enabled = True

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim clsConfig_Report As New config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            '  Dim index As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_SalesOrderPacking_Index").Value
            Dim index As String = "'0010000023137" & "',"
            'For i As Integer = 0 To grdPackingSoGroupView.Rows.Count - 1
            '    index &= "'" & Me.grdPackingSoGroupView.Rows(i).Cells("Col_SalesOrderPacking_index2").Value & "',"
            'Next

            Dim frmReport2 As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal2 As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            oCrystal = clsPacking.GetReportInfo("rptTransferDocRange", " and SalesOrderPacking_index in (" & index.Trim().Substring(0, index.Length - 1) & ") ")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            If clsPacking.SavePackingWithdraw(CType(grdData.DataSource, DataTable), Me._Serial_Data, txtCustomerShipping_Location.Text.Trim) Then
                Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
                Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                oCrystal = clsPacking.GetReportInfoWithdrawBox("rptTransferPackingDocRange", "")

                frmReport.CrystalReportViewer1.ReportSource = oCrystal
                frmReport.ShowDialog()


                oCrystal = clsPacking.GetReportInfoWithdrawBox("rptInsertTransport", "")
                frmReport.CrystalReportViewer1.ReportSource = oCrystal
                frmReport.ShowDialog()



                'W_MSG_Information("บันทึกเสร็จสิ้น")
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
End Class