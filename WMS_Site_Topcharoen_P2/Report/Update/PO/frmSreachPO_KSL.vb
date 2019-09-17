Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports System.Configuration.ConfigurationSettings
Imports System.Configuration
Imports System.Threading
Imports System.Globalization
Public Class frmSreachPO_KSL


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Dim strWhere As String = ""
            If chkCustomer.Checked Then
                strWhere &= String.Format(" And Customer_index = '{0}' ", txtCustomerId.Tag)
            End If
            If chkWarehouse.Checked Then
                strWhere &= String.Format(" And Warehouse_Index = '{0}' ", cboDistri.SelectedValue)
            End If
            If chkdate.Checked Then
                strWhere &= String.Format("  And (PurchaseOrder_Date  >= '{0}' And PurchaseOrder_Date <= '{1}') ", dtStartDate.Value.ToString("yyyy-MM-dd"), dtEndDate.Value.ToString("yyyy-MM-dd"))
            End If
            If chkSupplier.Checked Then
                strWhere &= String.Format(" And Supplier_Index = '{0}' ", txtSupplierId.Tag)
            End If
            If chkSend.Checked Then
                strWhere &= String.Format("  And ( cast(Expected_Delivery_Date as date) >= '{0} 00:00:01' and   cast(Expected_Delivery_Date as date) <= '{0} 23:59:59')", CDate(dtExpire.Text).ToString("yyyy-MM-dd"))
                'strWhere &= String.Format(" And Expected_Delivery_Date = '{0}' ", CDate(dtExpire.Text).ToString("yyyy-MM-dd"))
            End If
            If chkRemark.Checked Then
                strWhere &= String.Format(" And Remark = '{0}' ", txtRemark.Text)
            End If
            Dim Report_Name = "RPT_KSL_PO"
            Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
            Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, strWhere)
            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            cry = oReport.LoadReport()
            cry.SetParameterValue("Bdate", dtStartDate.Value.ToString("dd-M-yy"))
            cry.SetParameterValue("Edate", dtEndDate.Value.ToString("dd-M-yy"))


            frm.CrystalReportViewer1.ReportSource = cry

            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplier.Click
        Try
            Dim frm As New frmSupplier_Popup
            frm.ShowDialog()
            Dim tmpSupplier_Index As String = ""
            tmpSupplier_Index = frm.Supplier_Index

            If tmpSupplier_Index = "" Then
                Exit Sub
            End If

            If Not tmpSupplier_Index = "" Then
                Me.txtSupplierId.Tag = tmpSupplier_Index
                Me.txtSupplierId.Text = frm.strSupplier_Id
                Me.txtSupplierDes.Text = frm.SupplierName
            Else
                Me.txtSupplierId.Tag = ""
                Me.txtSupplierId.Text = ""
                Me.txtSupplierDes.Text = ""
            End If
            ' *********************
            frm.Close()
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
    Private Sub frmSreachPO_KSL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            getDistributionCenter()
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

    Private Sub chkSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSupplier.CheckedChanged
        Try
            btnSupplier.Enabled = chkSupplier.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkSend_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSend.CheckedChanged
        Try
            dtExpire.Enabled = chkSend.Checked
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
End Class