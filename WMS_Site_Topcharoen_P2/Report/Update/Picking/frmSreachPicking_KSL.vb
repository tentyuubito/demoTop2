Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Public Class frmSreachPicking_KSL


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Dim strWhere As String = ""
            If chkCustomer.Checked Then
                strWhere &= String.Format(" And Customer_index = '{0}' ", txtCustomerId.Tag)
            End If
            If chkWarehouse.Checked Then
                strWhere &= String.Format(" And Warehouse_index = '{0}' ", txtWareHouserId.Tag)
            End If
            If chkdate.Checked Then
                strWhere &= String.Format("  And (Packing_date >= '{0}' And Packing_date <= '{1}') ", dtStartDate.Value.ToString("yyyy-MM-dd"), dtEndDate.Value.ToString("yyyy-MM-dd"))
            End If
            If chkDocType.Checked Then
                strWhere &= String.Format(" And DocumentType_index = '{0}' ", cboDocType.SelectedValue)
            End If
            If chkStatus.Checked Then
                strWhere &= String.Format(" And Status = '{0}' ", cboStatus.SelectedValue)
            End If
         

            If chkRemark.Checked Then
                strWhere &= String.Format(" And Remark = '{0}' ", txtRemark.Text)
            End If
            Dim Report_Name = "RPT_KSL_Pick"
            Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
            Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, strWhere)
            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            cry = oReport.LoadReport()
            cry.SetParameterValue("Bdate", dtStartDate.Value.ToString("dd-M-yy"))
            cry.SetParameterValue("Edate", dtEndDate.Value.ToString("dd-M-yy"))
            cry.SetParameterValue("Warehouse", txtWareHouserId.Text)

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

    Private Sub btnWareHouser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWareHouser.Click
        Try
            Dim frm As New frmWareHouse_Popup
            frm.ShowDialog()
            ' --- Receive value มาแสดงในตัวแปล 
            Dim tmpWareHouse_index As String = ""
            tmpWareHouse_index = frm.WareHouse_index

            If tmpWareHouse_index = "" Then
                Exit Sub
            End If

            If Not tmpWareHouse_index = "" Then
                txtWareHouserId.Tag = tmpWareHouse_index
                txtWareHouserId.Text = frm.WareHouseName
                txtWareHouserDes.Text = frm.WareHouseDes
            Else
                Me.txtWareHouserId.Tag = ""
                Me.txtWareHouserId.Text = ""
                Me.txtWareHouserDes.Text = ""
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

    Private Sub chkWarehouse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            btnWareHouser.Enabled = chkWarehouse.Checked
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

    Private Sub getStatus()
        Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProcessStatus()
            objDT = objClassDB.DataTable

            With cboStatus
                .DisplayMember = "Description"
                .ValueMember = "Status"
                .DataSource = objDT
            End With

            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getDocType()
        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(2)
            objDT = objClassDB.DataTable

            Dim cbItem(1) As String
            cbItem(0) = "0"
            cbItem(1) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            With cboDocType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

            ' *************************************
            cboDocType.SelectedIndex = cboDocType.Items.Count - 1

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    Private Sub getProcessStatus()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProcessStatus()
            objDT = objClassDB.DataTable

            With cboStatus
                .DisplayMember = "Description"
                .ValueMember = "Status"
                .DataSource = objDT
            End With

            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    Private Sub frmSreachSO_KSL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            getProcessStatus()
            getStatus()
            getDocType()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkStocke_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStatus.CheckedChanged
        Try
            cboStatus.Enabled = chkStatus.Checked
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

    Private Sub chkWarehouse_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWarehouse.CheckedChanged
        Try
            btnWareHouser.Enabled = chkWarehouse.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class