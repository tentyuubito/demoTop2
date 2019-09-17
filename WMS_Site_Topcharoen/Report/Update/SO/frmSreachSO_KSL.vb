Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Public Class frmSreachSO_KSL


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Dim strWhere As String = ""

            If chkSo_No.Checked Then
                strWhere &= String.Format(" And SalesOrder_No = '{0}' ", txtSO_NO.Text)
            End If
            If chkCustomer.Checked Then
                strWhere &= String.Format(" And Customer_index = '{0}' ", txtCustomerId.Tag)
            End If
            If chkWarehouse.Checked Then
                strWhere &= String.Format(" And Warehouse_index = '{0}' ", cboDistri.SelectedValue)
            End If
            If chkdate.Checked Then
                strWhere &= String.Format("  And (SalesOrder_Date >= '{0}' And SalesOrder_Date  <= '{1}') ", dtStartDate.Value.ToString("yyyy-MM-dd"), dtEndDate.Value.ToString("yyyy-MM-dd"))
            End If
            If chkDocType.Checked Then
                strWhere &= String.Format(" And DocumentType_index = '{0}' ", cboDocType.SelectedValue)
            End If
            If chkSKU_Id.Checked Then
                'strWhere &= String.Format(" And Status_WH = '{0}' ", cboStocke.SelectedIndex)
                strWhere &= String.Format(" And Sku_index = '{0}' ", txtSku_id.Tag)
            End If
            If chkPayment.Checked Then
                strWhere &= String.Format(" And Status_Payment = '{0}' ", cboPayment.SelectedIndex)
            End If

            If chkRemark.Checked Then
                strWhere &= String.Format(" And Remark = '{0}' ", txtRemark.Text)
            End If
            Dim Report_Name = "RPT_KSL_SO_RM"
            Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
            Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, strWhere)
            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            cry = oReport.LoadReport()
            cry.SetParameterValue("Bdate", dtStartDate.Value.ToString("dd-M-yy"))
            cry.SetParameterValue("Edate", dtEndDate.Value.ToString("dd-M-yy"))
            cry.SetParameterValue("Warehouse", cboDistri.Text)

            frm.CrystalReportViewer1.ReportSource = cry

            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Try

            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            ' --- Receive value มาแสดงในตัวแปล 
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index

            If tmpCustomer_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Index = "" Then
                txtCustomerId.Tag = tmpCustomer_Index
                txtCustomerId.Text = frm.strCustomer_Name_Id
                txtCustomerDes.Text = frm.customerName
            Else
                txtCustomerId.Tag = ""
                Me.txtCustomerId.Text = ""
                Me.txtCustomerDes.Text = ""
            End If

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub





    Private Sub chkRemark_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRemark.CheckedChanged
        Try
            txtRemark.Enabled = chkRemark.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomer.CheckedChanged
        Try
            btnCustomer.Enabled = chkCustomer.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkWarehouse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWarehouse.CheckedChanged
        Try
            cboDistri.Enabled = chkWarehouse.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged
        Try
            dtStartDate.Enabled = chkdate.Checked
            dtEndDate.Enabled = chkdate.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkDocType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDocType.CheckedChanged
        Try
            cboDocType.Enabled = chkDocType.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub getDocType()
        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        objDT = objClassDB.DBExeQuery("Select * from ms_documentType where process_id = 10")
        Try
            With cboDocType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
        End Try
    End Sub
    Private Sub getDistributionCenter()
        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        Dim strSQL As String = ""
        strSQL = "select * from ms_DistributionCenter where status_id <> -1"
        objDT = objClassDB.DBExeQuery(strSQL)
        Try
            With cboDistri
                .DisplayMember = "Description"
                .ValueMember = "DistributionCenter_Index"
                .DataSource = objDT
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
        End Try
    End Sub
    Private Sub frmSreachSO_KSL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            getDistributionCenter()
            getDocType()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkStocke_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSku_id.CheckedChanged
        Try
            btnSku.Enabled = chkSku_id.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkPayment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPayment.CheckedChanged
        Try
            cboPayment.Enabled = chkPayment.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboDistri_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDistri.SelectedIndexChanged

    End Sub

  
  
    Private Sub btnSku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.SEARCH, "")
            frm.ShowDialog()
            ' --- Receive value มาแสดงในตัวแปล 
            Dim tmpSku_Index As String = ""
            tmpSku_Index = frm.Sku_Index

            If tmpSku_Index = "" Then
                Exit Sub
            End If

            If Not tmpSku_Index = "" Then
                txtSku_id.Tag = tmpSku_Index
                txtSku_id.Text = frm.Sku_ID
                txtSkuDes.Text = frm.Sku_Des_th
            Else
                Me.txtSku_id.Tag = ""
                Me.txtSku_id.Text = ""
                Me.txtSkuDes.Text = ""
            End If

            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chPo_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSo_No.CheckedChanged
        Try
            txtSO_NO.Enabled = chkSo_No.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class